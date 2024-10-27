
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool gameIsActive { get; private set; }
    public bool arenaIsActive { get; private set; }
    public PlayerScript player { get; private set; }
    public BossScript enemy { get; private set; }

    public MovePointPrefabScript[] points => arenaManager.movePointsArray;

    [SerializeField] GameSettings settings;

    [Header("UI Part")]

    [SerializeField] UIController uiController;

    [Header("Arena Part")]

    [SerializeField] ArenaManager arenaManager;
    [SerializeField] Transform projectileParent;
    [SerializeField] ProjectileBase projectileBase;

    private ProjectileManager projectileManager;
    private ArenaStatistic statistic;

    private float timerStart;

    void Awake()
    {
        Instance = this;
        gameIsActive = false;

        GameEventBus.OnSomeoneDies += CheckWinner;

        statistic = new ArenaStatistic();
        statistic.UpdateStatistic(new int[] { 0, 0, 0 });
        projectileManager = new ProjectileManager(projectileBase, projectileParent);

        uiController.Init(this);
        arenaManager.Init(settings);
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

        UpdateStartTimer();

        Debug.Log("Game is Active = " + gameIsActive);
    }

    public void LoadLevel()
    {
        arenaManager.SpawnArena(statistic.arenaNum);
        player = arenaManager.SpawnPlayer(0);
        enemy = arenaManager.SpawnBoss(0);
        //enemy = arenaManager.SpawnBoss(statistic.bossNum);

        player.Init(enemy);
        enemy.Init(player);

        StartArenaTimer();
    }

    private void CheckWinner()
    {
        gameIsActive = false;
        timerStart = settings.timeToStart;

        if (player.isAlive)
            Debug.Log("Player Win");
        else
            Debug.Log("Player Defeat");

        uiController.CallEndScreen(player.isAlive);
        ClearArena();
    }

    public Transform GetNextWayPoint(int pointNum)
    {
        return points[pointNum].transform;
    }

    public List<MovePointPrefabScript> GetEmptyMovepointsList() { return arenaManager.GetEmptyMovepointsList(player.currentPointNum); }

    private void StartArenaTimer()
    {
        timerStart = settings.timeToStart;
    }

    private void UpdateStartTimer()
    {
        if (timerStart > 0)
        {
            timerStart -= Time.deltaTime;
            uiController.UpdateArenaTimer(timerStart);
        }
        else
        {
            if(!gameIsActive)
                StartArena();
        }
    }

    private void StartArena()
    {
        gameIsActive = true;
        enemy.ActivateBoss();
        uiController.StartArena();

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
        //timerStart = 4;
        //player.ResetPlayer();
        //enemy.ResetEnemy();
        //uiController.endMenu.SetActive(false);
        //ClearArena();

        SceneManager.LoadScene(0);
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
