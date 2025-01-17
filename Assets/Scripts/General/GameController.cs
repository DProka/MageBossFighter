
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int currentLvlNum { get; private set; }

    public bool gameIsActive { get; private set; }
    public PlayerScript player { get; private set; }
    public BossScript enemy { get; private set; }

    [SerializeField] GameSettings settings;

    [Header("UI Part")]

    [SerializeField] UIController uiController;

    [Header("Arena Part")]

    [SerializeField] ArenaManager arenaManager;
    [SerializeField] Transform projectileParent;
    [SerializeField] ProjectileManagerSettings projectileManagerSettings;

    private ProjectileManager projectileManager;
    //private BoosterManager boosterManager;
    private VfxManager vfxManager;

    private bool timerIsActive;
    private float timerStart;

    void Awake()
    {
        Instance = this;

        gameIsActive = false;
        timerIsActive = false;

        GameEventBus.OnSomeoneDies += CheckWinner;

        arenaManager.Init(settings);
        uiController.Init(this);

        projectileManager = new ProjectileManager(projectileManagerSettings, projectileParent);
        vfxManager = new VfxManager(settings.vfxBase);
        //boosterManager = new BoosterManager(arenaManager.movePointsArray);
        currentLvlNum = DataHolder.gameLevel;

        if(DataHolder.statsLvls == null)
        {
            DataHolder.statsLvls = new int[] { 1, 1, 1 };
        }

        CameraManager.Instance.SetCameraPosition(settings.cameraSettings);
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
    }

    public void LoadLevel()
    {
        player = arenaManager.SpawnPlayer(0);

        arenaManager.SpawnArena(currentLvlNum);
        enemy = arenaManager.SpawnBoss(currentLvlNum);
        uiController.SetBossName(enemy._settings.bossName);

        player.Init(enemy);
        enemy.Init(player);

        uiController.SwitchTutorialPart(true);
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

    public void StartArenaTimer()
    {
        timerStart = settings.timeToStart;
        timerIsActive = true;

        uiController.SwitchTutorialPart(false);
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

    public void InstantiatePlayerProjectile(Vector3 startPosition, float damage)
    {
        projectileManager.InstantiatePlayerProjectile(startPosition, enemy.transform.position, damage, player._settings.projectileSpeed);
    }
    
    public void InstantiateEnemyProjectile(Vector3 startPosition, Vector3 targetPosition)
    {
        projectileManager.InstantiateEnemyProjectile(startPosition, targetPosition, enemy._settings.attackProjectilePrefab, enemy._settings.damage, enemy._settings.projectileSpeed);
    }
    
    public void InstantiateEnemyPointProjectile(Vector3 targetPosition, int num)
    {
        projectileManager.InstantiateEnemyPointProjectile(targetPosition, enemy._settings.pointProjectilePrefab, enemy._settings.damage);
    }

    public void RestartScene()
    {
        timerStart = 4;
        timerIsActive = true;
        player.ResetPlayer();
        enemy.ResetEnemy();
        uiController.CloseEndScreen();
        ClearArena();
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

        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    private void OnDestroy()
    {
        GameEventBus.OnSomeoneDies -= CheckWinner;
    }

}
