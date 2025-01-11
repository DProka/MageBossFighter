
using UnityEngine;

[CreateAssetMenu(fileName = "MovePointSettings", menuName = "ScriptableObject/Arena/MovePointSettings")]
public class MovePointSettings : ScriptableObject
{
    [Header("Status Timers")]

    public float burningTime;
    public float freezeTime;
    public float blockedTime;
    public float attackTime = 0.5f;

    [Header("Status Colors")]

    public Color cleanColor;
    public Color attentionColor;
    public Color bonusColor;
    public Color blockedColor;

    [Header("Booster Timers")]

    public float healthTime = 120;
    public float attackDamageTime = 30;
    public float attackSpeedTime = 30;
    public float defenceTime = 30f;

    [Header("Circle Settings")]

    public Vector3 circlePunchSize;

    public Color circleAttackColor;
    public Color circleBonusColor;
}
