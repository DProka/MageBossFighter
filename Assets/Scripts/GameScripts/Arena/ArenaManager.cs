
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public MovePoint[] points { get; private set; }

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

    public void SpawnArena(int arenaNum)
    {
        Instantiate(lvlbase.arenaModel[arenaNum], arenaPivot.position, Quaternion.identity);
    }

    public BossController SpawnBoss(int bossNum)
    {
        GameObject boss = Instantiate(lvlbase.bossModel[bossNum], arenaPivot.position, Quaternion.Euler(0, 180, 0));
        BossController enemy = boss.GetComponent<BossController>();

        return enemy;
    }

    private void PrepareArena()
    {
        points = new MovePoint[pointsParent.childCount];

        for (int i = 0; i < pointsParent.childCount; i++)
        {
            points[i] = pointsParent.GetChild(i).GetComponent<MovePoint>();
        }
    }
}
