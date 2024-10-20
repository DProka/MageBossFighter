using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    [Header("Data Links")]

    [SerializeField] PrefabBase prefabBase;

    [Header("Main")]

    public int gameLvl;
    public int choosedArena;
    public int choosedBoss;
    public TouchControl touch;
    public UIController uiScript;
    public Transform arenaPivot;
    public Transform enemyPivot;
    public MovePoint[] points;
    public bool arenaIsActive;

    public float timeToStart;
    private float timerStart;

    [Header("Player Main")]

    public PlayerController player;
    public HealthBar playerHB;

    [HideInInspector]public List<PlayerBullet> playerBullets;
    
    [Header("Enemy Main")]

    public EnemyController enemy;
    public HealthBar enemyHB;

    [HideInInspector]public List<EnemyBullet> enemyBullets;

    void Awake()
    {
        gameController = this;
        arenaIsActive = false;
        timerStart = timeToStart;
        LoadLevel();
        player.Init();
        enemy.Init();
    }

    void Update()
    {
        uiScript.UpdateUI();

        if(arenaIsActive)
        {
            CheckArenaState();

            PlayerUpdate();
            EnemyUpdate();
        }
        else
        {
            if(timerStart > 0)
            {
                timerStart -= Time.deltaTime;
                ArenaStart();
            }

            ClearArena();
        }
    }

    public void LoadLevel()
    {
        gameLvl = DataHolder.gameLevel;
        choosedBoss = gameLvl;
        //SpawnArena();
        //SpawnBoss();

        Instantiate(prefabBase.arenaModel[0], arenaPivot.position, Quaternion.identity);
        enemy = Instantiate(prefabBase.bossModel[0], arenaPivot.position, Quaternion.Euler(0, 180, 0));

        uiScript.UpdateNames(prefabBase.BossName[0], prefabBase.lvlName[0]);
    }

    private void SpawnArena()
    {
        Instantiate(prefabBase.arenaModel[choosedArena], arenaPivot.position, Quaternion.identity);
    }

    private void SpawnBoss()
    {
        //GameObject boss = Instantiate(lvlbase.bossModel[choosedBoss], arenaPivot.position, Quaternion.Euler(0,180,0));
        //enemy = boss.GetComponent<EnemyController>();
    }

    public void CheckArenaState()
    {
        if(!enemy.isAlive)
        {
            Debug.Log("Player Win");
            uiScript.CallEndScreen(true);
            arenaIsActive = false;
        }

        if (!player.isAlive)
        {
            Debug.Log("Player Defeat");
            uiScript.CallEndScreen(false);
            arenaIsActive = false;
        }
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
            arenaIsActive = true;
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
        arenaIsActive = false;
        timerStart = 4;
        player.RestartPlayer();
        enemy.ResetEnemy();
        uiScript.endMenu.SetActive(false);
        ClearArena();
    }
}
