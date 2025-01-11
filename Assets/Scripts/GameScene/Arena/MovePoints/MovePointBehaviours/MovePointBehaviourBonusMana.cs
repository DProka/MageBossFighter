using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointBehaviourBonusMana : IMovePointBehaviour
{
    private ArenaManager arenaManager;
    private MovePointPrefabScript currentPoint;

    public MovePointBehaviourBonusMana(ArenaManager _arenaManager)
    {
        arenaManager = _arenaManager;
    }

    public void Enter(MovePointPrefabScript _currentPoint)
    {
        currentPoint = _currentPoint;
    }

    void IMovePointBehaviour.Update()
    {

    }

    public void Exit()
    {

    }

}
