
using UnityEngine;

public class BossBehaviourBlockMovepoint : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;
    private int attackCounter;

    public BossBehaviourBlockMovepoint(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;
    }

    public void Exit()
    {
        attackTimer = unit._settings.delayBeforeAttack;
    }

    void IBossBehaviour.Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else
            BlockPoint();
    }

    private void BlockPoint()
    {
        attackTimer = settings.attackSpeed;
        int[] points = ArenaSupportScript.GetTwoNextClosestPoints();
        int random = Random.Range(0, points.Length);
        MovePointPrefabScript targetPoint = ArenaManager.Instance.GetMovePointByNum(points[random]);
        targetPoint.SetNewStatus(ArenaManager.PointStatus.Blocked);

        GameController.Instance.InstantiateEnemyPointProjectile(targetPoint.transform.position, 1);
        attackCounter--;

        if (attackCounter <= 0)
            unit.SetRandomBehaviour();
    }
}
