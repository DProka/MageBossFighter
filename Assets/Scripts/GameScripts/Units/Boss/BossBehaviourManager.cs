using System;
using System.Collections.Generic;

public class BossBehaviourManager
{
    private BossScript thisUnit;
    private Dictionary<Type, IBossBehaviour> behavioursMap;
    private IBossBehaviour behaviourCurrent;

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
        IBossBehaviour newBeh = GetBehaviour<BossBehaviourIdle>();

        switch (behaviour)
        {
            case Behaviour.Idle:
                newBeh = GetBehaviour<BossBehaviourIdle>();
                break;

            case Behaviour.SimpleAttack:
                newBeh = GetBehaviour<BossBehaviourSimpleAttack>();
                break;
        
            case Behaviour.SpiralAttack:
                newBeh = GetBehaviour<BossBehaviourSpiralAttack>();
                break;
        
            case Behaviour.RoundAttack:
                newBeh = GetBehaviour<BossBehaviourRoundAttack>();
                break;
        
            case Behaviour.EvenOddAttack:
                newBeh = GetBehaviour<BossBehaviourEvenOddAttack>();
                break;
        
                //-----------------------------------------------------------------

            case Behaviour.BurnMovepoint:
                newBeh = GetBehaviour<BossBehaviourBurnMovepoint>();
                break;
        
            case Behaviour.FreezeMovepoint:
                newBeh = GetBehaviour<BossBehaviourFreezeMovepoint>();
                break;
        
            case Behaviour.BlockMovepoint:
                newBeh = GetBehaviour<BossBehaviourBlockMovepoint>();
                break;
        }

        SetBehaviour(newBeh);
    }

    public enum Behaviour
    {
        Idle,
        SimpleAttack,
        SpiralAttack,
        RoundAttack,
        EvenOddAttack,

        BurnMovepoint,
        FreezeMovepoint,
        BlockMovepoint,
    }

    private void SetBehaviour(IBossBehaviour newBehaviour)
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
        behavioursMap = new Dictionary<Type, IBossBehaviour>();

        behavioursMap[typeof(BossBehaviourIdle)] = new BossBehaviourIdle(thisUnit);
        behavioursMap[typeof(BossBehaviourSimpleAttack)] = new BossBehaviourSimpleAttack(thisUnit);
        behavioursMap[typeof(BossBehaviourSpiralAttack)] = new BossBehaviourSpiralAttack(thisUnit);
        behavioursMap[typeof(BossBehaviourRoundAttack)] = new BossBehaviourRoundAttack(thisUnit);
        behavioursMap[typeof(BossBehaviourEvenOddAttack)] = new BossBehaviourEvenOddAttack(thisUnit);

        behavioursMap[typeof(BossBehaviourBurnMovepoint)] = new BossBehaviourBurnMovepoint(thisUnit);
        behavioursMap[typeof(BossBehaviourFreezeMovepoint)] = new BossBehaviourFreezeMovepoint(thisUnit);
        behavioursMap[typeof(BossBehaviourBlockMovepoint)] = new BossBehaviourBlockMovepoint(thisUnit);

        SetNewBehaviour(Behaviour.Idle);
    }

    private IBossBehaviour GetBehaviour<T>() where T : IBossBehaviour
    {
        var type = typeof(T);
        return behavioursMap[type];
    }
}
