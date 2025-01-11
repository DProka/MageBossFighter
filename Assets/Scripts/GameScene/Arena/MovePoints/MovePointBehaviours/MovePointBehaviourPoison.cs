using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointBehaviourPoison : IMovePointBehaviour
{
    private ArenaManager arenaManager;
    private MovePointPrefabScript currentPoint;

    public MovePointBehaviourPoison(ArenaManager _arenaManager)
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
