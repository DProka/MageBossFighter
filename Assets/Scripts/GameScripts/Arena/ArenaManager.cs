
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public MovePointPrefabScript[] movePointsArray { get; private set; }

    [Header("Move Point Settings")]

    [SerializeField] MovePointSettings movePointSettings;

    [Header("Spawn Points")]

    [SerializeField] Transform pointsParent;
    [SerializeField] Transform arenaPivot;
    [SerializeField] Transform playerPivot;
    [SerializeField] Transform enemyPivot;

    private LevelBase lvlbase;

    public void Init(LevelBase _lvlbase)
    {
        lvlbase = _lvlbase;
        PrepareArena();
    }

    public void UpdateArena()
    {
        UpdateMovePoints();
    }

    public void SpawnArena(int arenaNum) => Instantiate(lvlbase.arenaPrefab[arenaNum], arenaPivot.position, Quaternion.identity, transform);
    
    public PlayerScript SpawnPlayer(int prefNum)
    {
        GameObject pref = Instantiate(lvlbase.playerPrefab[prefNum], movePointsArray[0].transform.position, Quaternion.Euler(0, 0, 0), transform);
        PlayerScript player = pref.GetComponent<PlayerScript>();

        return player;
    }
    
    public BossScript SpawnBoss(int bossNum)
    {
        GameObject boss = Instantiate(lvlbase.bossPrefab[bossNum], arenaPivot.position, Quaternion.Euler(0, 180, 0), transform);
        BossScript enemy = boss.GetComponent<BossScript>();

        return enemy;
    }

    public List<MovePointPrefabScript> GetEmptyMovepointsList()
    {
        List<MovePointPrefabScript> list = new List<MovePointPrefabScript>();

        for (int i = 0; i < movePointsArray.Length; i++)
        {
            if (movePointsArray[i].pointStatus == MovePointPrefabScript.Status.NoStatus)
                list.Add(movePointsArray[i]);
        }

        return list;
    }

    private void PrepareArena()
    {
        movePointsArray = new MovePointPrefabScript[pointsParent.childCount];

        for (int i = 0; i < pointsParent.childCount; i++)
        {
            movePointsArray[i] = pointsParent.GetChild(i).GetComponent<MovePointPrefabScript>();
            movePointsArray[i].Init(movePointSettings);
        }
    }

    private void UpdateMovePoints()
    {
        foreach(MovePointPrefabScript point in movePointsArray)
        {
            point.UpdateScript();
        }
    }
}
