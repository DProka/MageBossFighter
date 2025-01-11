using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointBehaviourBurn : IMovePointBehaviour
{
    private ArenaManager arenaManager;
    private MovePointPrefabScript currentPoint;
    private MovePointSettings settings;

    private float timer;

    public MovePointBehaviourBurn(ArenaManager _arenaManager, MovePointSettings _settings)
    {
        arenaManager = _arenaManager;
        settings = _settings;
    }

    public void Enter(MovePointPrefabScript _currentPoint)
    {
        currentPoint = _currentPoint;
        timer = settings.burningTime;
    }

    void IMovePointBehaviour.Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Exit();
        }
    }

    public void Exit()
    {
        currentPoint.SetNewStatus(ArenaManager.PointStatus.NoStatus);
    }

}
