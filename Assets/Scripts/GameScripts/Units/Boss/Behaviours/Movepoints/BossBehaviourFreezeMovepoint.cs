
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourFreezeMovepoint : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;

    public BossBehaviourFreezeMovepoint(BossScript thisUnit, EnemySkillSettings _settings)
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
            ChoosePoint();
    }

    void ChoosePoint()
    {
        attackTimer = settings.attackSpeed;
        List<MovePointPrefabScript> points = GameController.Instance.GetEmptyMovepointsList();

        int random = Random.Range(0, points.Count);
        points[random].SetNewStatus(MovePointPrefabScript.Status.Freeze);
    }
}
