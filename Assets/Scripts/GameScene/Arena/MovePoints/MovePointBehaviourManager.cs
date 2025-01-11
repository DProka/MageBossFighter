using System;
using System.Collections.Generic;
using UnityEngine;

public class MovePointBehaviourManager
{
    private ArenaManager arenaManager;
    private MovePointSettings settings;

    private Dictionary<Type, IMovePointBehaviour> behavioursMap;

    public MovePointBehaviourManager(ArenaManager _arenaManager, MovePointSettings _settings)
    {
        arenaManager = _arenaManager;
        settings = _settings;

        InitializeBehaviours();
    }

    public void SetNewBehToPointByNum(int num, ArenaManager.PointStatus behaviour)
    {
        SetNewBehaviour(num, behaviour);
    }

    private void SetNewBehaviour(int pointNum, ArenaManager.PointStatus status)
    {
        IMovePointBehaviour newBeh = GetBehaviour<MovePointBehaviourNoStatus>();

        switch (status)
        {
            case ArenaManager.PointStatus.NoStatus:
                newBeh = GetBehaviour<MovePointBehaviourNoStatus>();
                break;

            case ArenaManager.PointStatus.Burn:
                newBeh = GetBehaviour<MovePointBehaviourBurn>();
                break;

            case ArenaManager.PointStatus.Freeze:
                newBeh = GetBehaviour<MovePointBehaviourFreeze>();
                break;

            case ArenaManager.PointStatus.Poison:
                newBeh = GetBehaviour<MovePointBehaviourPoison>();
                break;

            case ArenaManager.PointStatus.ManaBonus:
                newBeh = GetBehaviour<MovePointBehaviourBonusMana>();
                break;
        }

        arenaManager.movePointsArray[pointNum].SetNewBehaviour(status, newBeh);
        Debug.Log("Point num = " + pointNum + " New status = " + status);
    }

    private IMovePointBehaviour GetBehaviour<T>() where T : IMovePointBehaviour
    {
        var type = typeof(T);
        return behavioursMap[type];
    }

    private void InitializeBehaviours()
    {
        behavioursMap = new Dictionary<Type, IMovePointBehaviour>();

        behavioursMap[typeof(MovePointBehaviourNoStatus)] = new MovePointBehaviourNoStatus(arenaManager);
        behavioursMap[typeof(MovePointBehaviourBurn)] = new MovePointBehaviourBurn(arenaManager, settings);
        behavioursMap[typeof(MovePointBehaviourFreeze)] = new MovePointBehaviourFreeze(arenaManager, settings);
        behavioursMap[typeof(MovePointBehaviourPoison)] = new MovePointBehaviourPoison(arenaManager);
        behavioursMap[typeof(MovePointBehaviourBonusMana)] = new MovePointBehaviourBonusMana(arenaManager);
    }
}
