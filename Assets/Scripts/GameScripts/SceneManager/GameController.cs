using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Main")]

    public int gameLvl;
    public int arenaNum;
    public int bossNum;
    public TouchControl touch;
    public UIController uiScript;
    public bool gameIsActive;

    public float timeToStart;
    private float timerStart;

    [Header("Arena Part")]

    [SerializeField] ArenaManager arenaManager;
    [SerializeField] LevelBase lvlbase;

    public MovePoint[] points => arenaManager.points;

    [SerializeField] Projectile projectilePrefab;
    private ProjectileManager projectileManager;

    [Header("Player Main")]

    public PlayerController player;
    public HealthBar playerHB;

    [HideInInspector]public List<PlayerBullet> playerBullets;
    
    [Header("Enemy Main")]

    public BossController enemy;
    public HealthBar enemyHB;

    [HideInInspector]public List<EnemyBullet> enemyBullets;

    void Awake()
    {
        Instance = this;
        gameIsActive = false;
        timerStart = timeToStart;

        GameEventBus.OnSomeoneDies += CheckWinner;

        projectileManager = new ProjectileManager(projectilePrefab);

        arenaManager.Init(lvlbase);
        LoadLevel();
        player.Init();
        enemy.Init();
    }

    void Update()
    {
        uiScript.UpdateUI();

        if (gameIsActive)
        {
            PlayerUpdate();
            EnemyUpdate();
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
        gameLvl = DataHolder.gameLevel;
        bossNum = gameLvl;
        arenaManager.SpawnArena(arenaNum);
        enemy = arenaManager.SpawnBoss(bossNum);
    }

    private void CheckWinner()
    {
        if(player.isAlive)
            Debug.Log("Player Win");
        else
            Debug.Log("Player Defeat");

        uiScript.CallEndScreen(player.isAlive);
        ClearArena();
        gameIsActive = false;
    }

    public void EnemyUpdate()
    {
        enemy.EnemyUpdate();

        for(int i = 0; i < enemyBullets.Count; i++)
        {
            enemyBullets[i].UpdateBullet();
        }
    }
    
    public void PlayerUpdate()
    {
        player.PlayerUpdate();
        touch.UpdateTouch();

        for (int i = 0; i < playerBullets.Count; i++)
        {
            playerBullets[i].UpdateBullet();
        }
    }

    void ArenaStart()
    {
        uiScript.timerObj.SetActive(true);
        int time = (int)timerStart;
        
        if(timerStart > 1)
            uiScript.timerText.text = " " + time;

        else if (timerStart <= 1 && timerStart > 0)
            uiScript.timerText.text = "GO";

        else
        {
            uiScript.timerObj.SetActive(false);
            gameIsActive = true;
        }
    }

    void ClearArena()
    {
        for (int i = 0; i < playerBullets.Count; i++)
        {
            playerBullets[i].DestroyBullet();
        }

        for (int i = 0; i < enemyBullets.Count; i++)
        {
            enemyBullets[i].DestroyBullet();
        }
    }

    public void RestartScene()
    {
        gameIsActive = false;
        timerStart = 4;
        player.RestartPlayer();
        enemy.ResetEnemy();
        uiScript.endMenu.SetActive(false);
        ClearArena();
    }

    public void InstantiateProjectile(Vector3 position, bool isPlayer)
    {
        UnitGeneral target = isPlayer ? enemy : player;
        projectileManager.InstantiateProjectile(position, target);
    }

    private void OnDestroy()
    {
        GameEventBus.OnSomeoneDies -= CheckWinner;
    }
}
