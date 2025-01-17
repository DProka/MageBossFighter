
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourBurnMovepoint : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;
    private float attackTimer;

    public BossBehaviourBurnMovepoint(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
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
            BurnPoint();
    }

    private void BurnPoint()
    {
        attackTimer = settings.attackSpeed;
        List<MovePointPrefabScript> points = ArenaManager.Instance.GetEmptyMovepointsList();

        int random = Random.Range(0, points.Count);
        points[random].SetNewStatus(ArenaManager.PointStatus.Burn);

        unit.SetRandomBehaviour();
    }
}
