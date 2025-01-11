using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointBehaviourFreeze : IMovePointBehaviour
{
    private ArenaManager arenaManager;
    private MovePointPrefabScript currentPoint;
    private MovePointSettings settings;

    private float timer;

    public MovePointBehaviourFreeze(ArenaManager _arenaManager, MovePointSettings _settings)
    {
        arenaManager = _arenaManager;
        settings = _settings;
    }

    public void Enter(MovePointPrefabScript _currentPoint)
    {
        currentPoint = _currentPoint;
        timer = settings.burningTime;

        currentPoint.SetVfxByStatus(ArenaManager.PointStatus.Freeze);
    }

    void IMovePointBehaviour.Update()
    {
        if (timer > 0)
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
        arenaManager.ResetPointStatusByNum(currentPoint.id);
    }
}
