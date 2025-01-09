
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObject/Game/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Main")]

    public float maxHealth = 100f;

    [Header("Moving")]

    public float moveSpeed = 20f;
    public float moveDelay;

    [Header("Attack")]

    public float damage = 10f;
    public float attackDelay = 0.5f;
    public float deforeAttackDelay = 0.2f;
    public float projectileSpeed = 10f;
    public int maxComboPoints = 30;

    [Header("Statuses")]

    public float burnStatusTime = 10f;
    public float burnDamage = 2f;
    public float burnDamageTime = 1f;

    public float freezeStatusTime = 10f;
    public float freezeDamage = 0.5f;
    public float freezeDamageTime = 0.5f;
    public float freezeSpeedFactor = 3f;
}
