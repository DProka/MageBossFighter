using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBonusMana : MonoBehaviour, IPointBonus
{
    [SerializeField] SpriteRenderer circleRenderer;

    public void Initialize()
    {

    }

    void IPointBonus.Update()
    {

    }

    public void Destroy()
    {
        
    }
}
