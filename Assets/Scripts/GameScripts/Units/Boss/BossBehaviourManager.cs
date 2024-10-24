using System;
using System.Collections.Generic;

public class BossBehaviourManager
{
    private BossScript thisUnit;
    private Dictionary<Type, IBehaviour> behavioursMap;
    private IBehaviour behaviourCurrent;

    public BossBehaviourManager(BossScript unit)
    {
        thisUnit = unit;
        InitializeBehaviours();
    }

    public void UpdateManager()
    {
        if (behaviourCurrent != null)
            behaviourCurrent.Update();
    }

    public void SetNewBehaviour(Behaviour behaviour)
    {
        IBehaviour newBeh = GetBehaviour<BossBehaviourIdle>();

        switch (behaviour)
        {
            case Behaviour.Idle:
                newBeh = GetBehaviour<BossBehaviourIdle>();
                break;

            case Behaviour.SimpleAttack:
                newBeh = GetBehaviour<BossBehaviourSimpleAttack>();
                break;
        
            case Behaviour.RoundAttack:
                newBeh = GetBehaviour<BossBehaviourRoundAttack>();
                break;
        }

        SetBehaviour(newBeh);
    }

    public enum Behaviour
    {
        Idle,
        SimpleAttack,
        RoundAttack,
    }

    private void SetBehaviour(IBehaviour newBehaviour)
    {
        if (behaviourCurrent != newBehaviour)
        {
            if (behaviourCurrent != null)
                behaviourCurrent.Exit();

            behaviourCurrent = newBehaviour;
            behaviourCurrent.Enter();
        }
    }

    private void InitializeBehaviours()
    {
        behavioursMap = new Dictionary<Type, IBehaviour>();

        behavioursMap[typeof(BossBehaviourIdle)] = new BossBehaviourIdle(thisUnit);
        behavioursMap[typeof(BossBehaviourSimpleAttack)] = new BossBehaviourSimpleAttack(thisUnit);
        behavioursMap[typeof(BossBehaviourRoundAttack)] = new BossBehaviourRoundAttack(thisUnit);

        SetNewBehaviour(Behaviour.Idle);
    }

    private IBehaviour GetBehaviour<T>() where T : IBehaviour
    {
        var type = typeof(T);
        return behavioursMap[type];
    }
}
