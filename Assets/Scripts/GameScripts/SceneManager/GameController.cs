
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool gameIsActive { get; private set; }
    public PlayerScript player { get; private set; }
    public BossScript enemy { get; private set; }

    public MovePointPrefabScript[] points => arenaManager.movePointsArray;

    [SerializeField] GameSettings settings;

    [Header("UI Part")]

    [SerializeField] UIController uiController;

    [Header("Arena Part")]

    [SerializeField] ArenaManager arenaManager;
    [SerializeField] LevelBase lvlbase;
    [SerializeField] ProjectileBase projectileBase;

    private ProjectileManager projectileManager;
    private ArenaStatistic statistic;

    private float timerStart;

    void Awake()
    {
        Instance = this;
        gameIsActive = false;
        timerStart = settings.timeToStart;

        GameEventBus.OnSomeoneDies += CheckWinner;

        statistic = new ArenaStatistic();
        statistic.UpdateStatistic(new int[] { 0, 0, 0 });
        projectileManager = new ProjectileManager(projectileBase);

        uiController.Init(this);
        arenaManager.Init(lvlbase);
        LoadLevel();
    }

    void Update()
    {
        uiController.UpdateUI();

        if (gameIsActive)
        {
            player.PlayerUpdate();
            enemy.EnemyUpdate();
            projectileManager.UpdateList();
            arenaManager.UpdateArena();
        }
        else
        {
            UpdateStartTimer();
        }
    }

    public void LoadLevel()
    {
        arenaManager.SpawnArena(statistic.arenaNum);
        player = arenaManager.SpawnPlayer(0);
        enemy = arenaManager.SpawnBoss(statistic.bossNum);

        player.Init(enemy);
        enemy.Init(player);

        StartArenaTimer();
    }

    private void CheckWinner()
    {
        if (player.isAlive)
            Debug.Log("Player Win");
        else
            Debug.Log("Player Defeat");

        uiController.CallEndScreen(player.isAlive);
        ClearArena();
        gameIsActive = false;
    }

    public Transform GetNextWayPoint(int pointNum)
    {
        return points[pointNum].transform;
    }

    public List<MovePointPrefabScript> GetEmptyMovepointsList() { return arenaManager.GetEmptyMovepointsList(); }

    private void StartArenaTimer()
    {
        uiController.timerObj.SetActive(true);
        timerStart = settings.timeToStart;
    }

    private void UpdateStartTimer()
    {
        if (timerStart > 0)
        {
            timerStart -= Time.deltaTime;
            int time = (int)timerStart;

            if (timerStart > 1)
                uiController.timerText.text = " " + time;

            else if (timerStart <= 1 && timerStart > 0)
                uiController.timerText.text = "GO";
        }
        else
        {
            StartArena();
        }
    }

    private void StartArena()
    {
        uiController.timerObj.SetActive(false);
        gameIsActive = true;
        enemy.ActivateBoss();

        Debug.Log("Arena is Active = " + gameIsActive);
    }

    private void ClearArena()
    {
        projectileManager.ClearList();
    }

    public void InstantiateProjectile(Vector3 startPosition, Vector3 targetPosition, bool isPlayer)
    {
        projectileManager.InstantiateProjectile(startPosition, targetPosition, isPlayer);
    }

    public void RestartScene()
    {
        gameIsActive = false;
        timerStart = 4;
        player.ResetPlayer();
        enemy.ResetEnemy();
        uiController.endMenu.SetActive(false);
        ClearArena();
    }

    public void GoToMaiuMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        GameEventBus.OnSomeoneDies -= CheckWinner;
    }

}
