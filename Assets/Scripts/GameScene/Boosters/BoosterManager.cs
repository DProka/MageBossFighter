using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager
{
    public BoosterManager Instance;

    public int attackDamageCount { get; private set; }
    public int attackSpeedCount { get; private set; }
    public int defenceCount { get; private set; }

    private MovePointPrefabScript[] movePointsArray;

    public BoosterManager(MovePointPrefabScript[] _movePointsArray)
    {
        Instance = this;
        movePointsArray = _movePointsArray;

        ResetBoosters();
    }

    public void GetBooster(Booster type)
    {
        switch (type)
        {
            case Booster.AttackDamage:
                attackDamageCount += 1;
                break;
        
            case Booster.AttackSpeed:
                attackSpeedCount += 1;
                break;
        
            case Booster.Defence:
                defenceCount += 1;
                break;
        }
    }

    public void SetBooster(Booster type)
    {
        int rendom = Random.Range(0, movePointsArray.Length);

        movePointsArray[rendom].SetBooster(type);
    }

    public void ResetBoosters()
    {
        attackDamageCount = 0;
        attackSpeedCount = 0;
        defenceCount = 0;
    }

    public enum Booster
    {
        Empty,
        Health,
        AttackDamage,
        AttackSpeed,
        Defence,
    }
}
