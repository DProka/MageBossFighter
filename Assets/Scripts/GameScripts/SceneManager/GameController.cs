
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool gameIsActive { get; private set; }
    public MovePoint[] points => arenaManager.movePointsArray;

    [SerializeField] GameSettings settings;

    [Header("UI Part")]

    [SerializeField] UIController uiController;

    [Header("Arena Part")]

    [SerializeField] ArenaManager arenaManager;
    [SerializeField] LevelBase lvlbase;
    [SerializeField] ProjectileBase projectileBase;

    private ProjectileManager projectileManager;
    private PlayerScript player;
    private BossScript enemy;
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
        }
        else
        {
            if (timerStart > 0)
            {
                timerStart -= Time.deltaTime;
                ArenaStart();
            }
        }
    }

    public void LoadLevel()
    {
        arenaManager.SpawnArena(statistic.arenaNum);
        player = arenaManager.SpawnPlayer(0);
        enemy = arenaManager.SpawnBoss(statistic.bossNum);

        player.Init(enemy);
        enemy.Init(player);
    }

    private void CheckWinner()
    {
        if(player.isAlive)
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

    void ArenaStart()
    {
        uiController.timerObj.SetActive(true);
        int time = (int)timerStart;
        
        if(timerStart > 1)
            uiController.timerText.text = " " + time;

        else if (timerStart <= 1 && timerStart > 0)
            uiController.timerText.text = "GO";

        else
        {
            uiController.timerObj.SetActive(false);
            gameIsActive = true;
        }
    }

    void ClearArena()
    {
        projectileManager.ClearList();
    }

    public void InstantiateProjectile(Vector3 position, bool isPlayer)
    {
        UnitGeneral target = isPlayer ? enemy : player;
        projectileManager.InstantiateProjectile(position, target, isPlayer);
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
