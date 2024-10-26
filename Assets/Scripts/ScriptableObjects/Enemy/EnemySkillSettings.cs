
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySkillSettings", menuName = "ScriptableObject/Boss/EnemySkillSettings")]
public class EnemySkillSettings : ScriptableObject
{
    public int attackCounter = 0;
    public float attackSpeed = 1f;
}
