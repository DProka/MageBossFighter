
using UnityEngine;

[CreateAssetMenu(fileName = "MovePointSettings", menuName = "ScriptableObject/Arena/MovePointSettings")]
public class MovePointSettings : ScriptableObject
{
    [Header("Timers")]

    public float burningTime;
    public float freezeTime;
    public float blockedTime;
    public float attackTime = 0.5f;

    [Header("Colors")]

    public Color startColor;
    public Color burnColor;
    public Color freezeColor;
    public Color blockedColor;
    public Color attackColor;
}
