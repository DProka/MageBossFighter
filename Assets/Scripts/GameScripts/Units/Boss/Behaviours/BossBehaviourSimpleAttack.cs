using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourSimpleAttack : IBehaviour
{
    private BossScript unit;

    public BossBehaviourSimpleAttack(BossScript thisUnit)
    {
        unit = thisUnit;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    void IBehaviour.Update()
    {
        throw new System.NotImplementedException();
    }
}
