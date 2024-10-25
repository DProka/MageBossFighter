
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourBlockMovepoint : IBossBehaviour
{
    private BossScript unit;
    private float attackTimer;

    public BossBehaviourBlockMovepoint(BossScript thisUnit)
    {
        unit = thisUnit;
    }

    public void Enter()
    {
        attackTimer = unit._settings.blockMovepointSpeed;
    }

    public void Exit()
    {

    }

    void IBossBehaviour.Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        else
        {
            ChoosePoint();
            attackTimer = unit._settings.blockMovepointSpeed;
        }
    }

    void ChoosePoint()
    {
        List<MovePointPrefabScript> points = GameController.Instance.GetEmptyMovepointsList();

        int random = Random.Range(0, points.Count);
        points[random].SetNewStatus(MovePointPrefabScript.Status.Blocked);
    }
}
