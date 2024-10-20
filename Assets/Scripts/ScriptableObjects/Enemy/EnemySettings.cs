
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObject/Game/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    public float health;
    public float damage;
    public float rotateSpeed;
}
