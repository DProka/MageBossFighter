using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovePointSettings", menuName = "ScriptableObject/Arena/MovePointSettings")]
public class MovePointSettings : ScriptableObject
{
    public float burningTime;
    public float freezeTime;
    public float blockedTime;

    public Material startMaterial;
    public Material burnMaterial;
    public Material freezeMaterial;
    public Material blockedMaterial;
}
