
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public MovePointPrefabScript[] movePointsArray { get; private set; }

    [Header("Move Point Settings")]

    [SerializeField] MovePointSettings movePointSettings;

    [Header("Spawn Points")]

    [SerializeField] Transform pointsParent;
    [SerializeField] Transform arenaParent;
    [SerializeField] Transform playerParent;
    [SerializeField] Transform enemyParent;

    private GameSettings settings;

    public void Init(GameSettings _settings)
    {
        settings = _settings;
        PrepareArena();
    }

    public void UpdateArena()
    {
        UpdateMovePoints();
    }

    public void SpawnArena(int arenaNum) => Instantiate(settings.arenaBase.arenaPrefabsArray[arenaNum], arenaParent.position, Quaternion.identity, arenaParent);
    
    public PlayerScript SpawnPlayer(int prefNum)
    {
        GameObject pref = Instantiate(settings.playerPrefab[prefNum], movePointsArray[0].transform.position, Quaternion.Euler(0, 0, 0), playerParent);
        PlayerScript player = pref.GetComponent<PlayerScript>();

        return player;
    }
    
    public BossScript SpawnBoss(int bossNum)
    {
        GameObject boss = Instantiate(settings.bossBase.bossPrefabsArray[bossNum], arenaParent.position, Quaternion.Euler(0, 180, 0), enemyParent);
        BossScript enemy = boss.GetComponent<BossScript>();

        return enemy;
    }

    public List<MovePointPrefabScript> GetEmptyMovepointsList(int playerPointNum)
    {
        List<MovePointPrefabScript> list = new List<MovePointPrefabScript>();

        int[] blockedNumbers = new int[] { playerPointNum - 1, playerPointNum, playerPointNum + 1 };
        for (int i = 0; i < blockedNumbers.Length; i++)
        {
            if (blockedNumbers[i] < 0)
                blockedNumbers[i] = movePointsArray.Length - 1;
            else if (blockedNumbers[i] >= movePointsArray.Length)
                blockedNumbers[i] = 0;
        }

        for (int i = 0; i < movePointsArray.Length; i++)
        {
            if (movePointsArray[i].currentStatus == MovePointPrefabScript.Status.NoStatus && !blockedNumbers.Contains(i))
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
            movePointsArray[i].Init(movePointSettings, i);
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
