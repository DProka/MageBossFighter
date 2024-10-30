
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

    private int currentLvlNum;
    private bool timerIsActive;
    private float timerStart;

    void Awake()
    {
        Instance = this;
        gameIsActive = false;
        timerIsActive = false;

        GameEventBus.OnSomeoneDies += CheckWinner;

        projectileManager = new ProjectileManager(projectileBase, projectileParent);

        currentLvlNum = DataHolder.gameLevel;
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

        //Debug.Log("Game is Active = " + gameIsActive);
    }

    public void LoadLevel()
    {
        player = arenaManager.SpawnPlayer(0);

        arenaManager.SpawnArena(currentLvlNum);
        enemy = arenaManager.SpawnBoss(currentLvlNum);

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

        uiController.CallEndScreen(true);
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
        timerIsActive = true;
    }

    private void UpdateStartTimer()
    {
        if (timerIsActive)
        {
            if (timerStart > 0)
            {
                timerStart -= Time.deltaTime;
                uiController.UpdateArenaTimer(timerStart);
            }
            else
            {
                timerIsActive = false;
                StartArena();
            }
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

    public void InstantiateProjectile(Vector3 startPosition, Vector3 targetPosition, bool isPlayer, float damage)
    {
        projectileManager.InstantiateProjectile(startPosition, targetPosition, isPlayer, damage);
    }

    public void RestartScene()
    {
        timerStart = 4;
        timerIsActive = true;
        player.ResetPlayer();
        enemy.ResetEnemy();
        uiController.CloseEndScreen();
        ClearArena();

        //SceneManager.LoadScene(0);
    }

    public void SwitchPauseGame()
    {
        if (timerStart <= 0)
        {
            if (gameIsActive)
            {
                gameIsActive = false;
                uiController.SwitchPauseGame(true);
            }
            else
            {
                gameIsActive = true;
                uiController.SwitchPauseGame(false);
            }
        }
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