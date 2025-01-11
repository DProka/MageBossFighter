using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointBehaviourNoStatus : IMovePointBehaviour
{
    private ArenaManager arenaManager;
    private MovePointPrefabScript currentPoint;

    public MovePointBehaviourNoStatus(ArenaManager _arenaManager)
    {
        arenaManager = _arenaManager;
    }

    public void Enter(MovePointPrefabScript _currentPoint)
    {
        currentPoint = _currentPoint;

        //currentPoint.SetVfxByStatus(ArenaManager.PointStatus.NoStatus);
    }

    void IMovePointBehaviour.Update()
    {

    }

    public void Exit()
    {
        
    }

}
