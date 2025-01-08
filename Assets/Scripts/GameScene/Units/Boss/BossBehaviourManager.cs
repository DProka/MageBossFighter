using System;
using System.Collections.Generic;
using System.Linq;

public class BossBehaviourManager
{
    private BossScript thisUnit;
    private EnemySettings settings;
    private Dictionary<Type, IBossBehaviour> behavioursMap;
    private IBossBehaviour behaviourCurrent;

    public BossBehaviourManager(BossScript unit, EnemySettings _settings)
    {
        thisUnit = unit;
        settings = _settings;
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
        
            case Behaviour.SpiralDoubleAttack:
                newBeh = GetBehaviour<BossBehaviourDoubleSpiralAttack>();
                break;
        
            case Behaviour.SpiralQuadrupleAttack:
                newBeh = GetBehaviour<BossBehaviourQuadrupleSpiralAttack>();
                break;
        
            case Behaviour.RoundAttack:
                newBeh = GetBehaviour<BossBehaviourRoundAttack>();
                break;
        
            case Behaviour.EvenOddAttack:
                newBeh = GetBehaviour<BossBehaviourEvenOddAttack>();
                break;
        
            case Behaviour.SectorAttack:
                newBeh = GetBehaviour<BossBehaviourSectorAttack>();
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
        
            case Behaviour.AttackMovepoint:
                newBeh = GetBehaviour<BossBehaviourAttackMovepoint>();
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
        SectorAttack,
        SpiralDoubleAttack,
        SpiralQuadrupleAttack,
        BurnMovepoint,
        FreezeMovepoint,
        BlockMovepoint,
        AttackMovepoint,
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

    private IBossBehaviour GetBehaviour<T>() where T : IBossBehaviour
    {
        var type = typeof(T);
        return behavioursMap[type];
    }

    private void InitializeBehaviours()
    {
        behavioursMap = new Dictionary<Type, IBossBehaviour>();

        behavioursMap[typeof(BossBehaviourIdle)] = new BossBehaviourIdle(thisUnit);
        behavioursMap[typeof(BossBehaviourSimpleAttack)] = new BossBehaviourSimpleAttack(thisUnit, GetBehaviourSettings(Behaviour.SimpleAttack));
        behavioursMap[typeof(BossBehaviourSpiralAttack)] = new BossBehaviourSpiralAttack(thisUnit, GetBehaviourSettings(Behaviour.SpiralAttack));
        behavioursMap[typeof(BossBehaviourDoubleSpiralAttack)] = new BossBehaviourDoubleSpiralAttack(thisUnit, GetBehaviourSettings(Behaviour.SpiralDoubleAttack));
        behavioursMap[typeof(BossBehaviourQuadrupleSpiralAttack)] = new BossBehaviourQuadrupleSpiralAttack(thisUnit, GetBehaviourSettings(Behaviour.SpiralQuadrupleAttack));
        behavioursMap[typeof(BossBehaviourRoundAttack)] = new BossBehaviourRoundAttack(thisUnit, GetBehaviourSettings(Behaviour.RoundAttack));
        behavioursMap[typeof(BossBehaviourEvenOddAttack)] = new BossBehaviourEvenOddAttack(thisUnit, GetBehaviourSettings(Behaviour.EvenOddAttack));
        behavioursMap[typeof(BossBehaviourSectorAttack)] = new BossBehaviourSectorAttack(thisUnit, GetBehaviourSettings(Behaviour.SectorAttack));

        behavioursMap[typeof(BossBehaviourBurnMovepoint)] = new BossBehaviourBurnMovepoint(thisUnit, GetBehaviourSettings(Behaviour.BurnMovepoint));
        behavioursMap[typeof(BossBehaviourFreezeMovepoint)] = new BossBehaviourFreezeMovepoint(thisUnit, GetBehaviourSettings(Behaviour.FreezeMovepoint));
        behavioursMap[typeof(BossBehaviourBlockMovepoint)] = new BossBehaviourBlockMovepoint(thisUnit, GetBehaviourSettings(Behaviour.BlockMovepoint));
        behavioursMap[typeof(BossBehaviourAttackMovepoint)] = new BossBehaviourAttackMovepoint(thisUnit, GetBehaviourSettings(Behaviour.AttackMovepoint));

        SetNewBehaviour(Behaviour.Idle);
    }

    private EnemySkillSettings GetBehaviourSettings(Behaviour behaviour)
    {
        if(settings.skillSettingsArray.Length > 0)
        {
            EnemySkillSettings newSettings = settings.skillSettingsArray[0];

            if (settings.skillTypesArray.Contains(behaviour))
            {
                int index = Array.FindIndex(settings.skillTypesArray, n => n == behaviour);
                newSettings = settings.skillSettingsArray[index];
            }

            return newSettings;
        }
        else
        {
            EnemySkillSettings newSettings = new EnemySkillSettings();

            return newSettings;
        }
    }
}
