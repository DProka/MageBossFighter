
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager Instance;

    public MovePointPrefabScript[] movePointsArray { get; private set; }

    [Header("Move Point Settings")]

    [SerializeField] MovePointSettings movePointSettings;

    [Header("Spawn Points")]

    [SerializeField] Transform pointsParent;
    [SerializeField] Transform arenaParent;
    [SerializeField] Transform playerParent;
    [SerializeField] Transform enemyParent;

    private GameSettings settings;
    private MovePointBehaviourManager pointBehaviourManager;

    private PointStatus[] pointStatusArray;

    public void Init(GameSettings _settings)
    {
        Instance = this;
        settings = _settings;

        pointBehaviourManager = new MovePointBehaviourManager(this, movePointSettings);

        PrepareMovePoints();
    }

    public void UpdateArena()
    {
        UpdateMovePoints();
    }

    public void SpawnArena(int arenaNum)
    {
        Instantiate(settings.arenaBase.arenaPrefabsArray[arenaNum], arenaParent.position, Quaternion.identity, arenaParent);
    }

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

    #region MovePoints

    public List<MovePointPrefabScript> GetEmptyMovepointsList()
    {
        int playerPointNum = GameController.Instance.player.currentPointNum;

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
            if (movePointsArray[i].currentStatus == PointStatus.NoStatus && !blockedNumbers.Contains(i))
                list.Add(movePointsArray[i]);
        }

        return list;
    }

    public MovePointPrefabScript GetMovePointByNum(int num) { return movePointsArray[num]; }
    
    public Vector3 GetMovePointPositionByNum(int num) { return movePointsArray[num].transform.position; }

    public Vector3 GetPlayerMovePointPosition() { return movePointsArray[GameController.Instance.player.currentPointNum].transform.position; }

    public void SetNewStatusToPointByNum(int num, PointStatus status)
    {
        pointStatusArray[num] = status;
        //movePointsArray[num].SetNewStatus(status);

        pointBehaviourManager.SetNewBehToPointByNum(num, status);
    }

    public void ResetPointStatusByNum(int num) => SetNewStatusToPointByNum(num, PointStatus.NoStatus);

    public PointStatus GetMovePointStatusByNum(int num) { return movePointsArray[num].currentStatus; }

    public bool CheckPointIsBlockedByNum(int num) { return movePointsArray[num].currentStatus == PointStatus.Blocked; }

    public enum PointStatus
    {
        NoStatus,
        Player,

        Burn,
        Freeze,
        Blocked,
        Poison,

        Attack,

        ManaBonus,
    }

    private void UpdateMovePoints()
    {
        foreach(MovePointPrefabScript point in movePointsArray)
        {
            point.UpdateScript();
        }
    }

    private void PrepareMovePoints()
    {
        movePointsArray = new MovePointPrefabScript[pointsParent.childCount];

        for (int i = 0; i < pointsParent.childCount; i++)
        {
            movePointsArray[i] = pointsParent.GetChild(i).GetComponent<MovePointPrefabScript>();
            movePointsArray[i].Init(movePointSettings, i);
        }

        pointStatusArray = new PointStatus[movePointsArray.Length];
    }

    #endregion
}
