
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObject/Game/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [Header("Main")]
    
    public float maxHealth;

    [Header("Attack")]

    public float damage;
    public float simpleAttackSpeed;
    public float roundAttackSpeed;

    [Header("Moving")]

    public float rotateSpeed;
}
