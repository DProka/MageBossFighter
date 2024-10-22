using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObject/Game/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Main")]

    public float maxHealth;

    [Header("Moving")]

    public float moveSpeed;
    public float moveDelay;

    [Header("Attack")]

    public float damage;
    public float attackDelay;
    public float deforeAttackDelay;
}
