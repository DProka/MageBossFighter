
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObject/Boss/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [Header("Main")]

    public string bossName;
    public float maxHealth;

    [Header("Attack")]

    public float damage;
    public float delayBeforeAttack = 2f;
    public float projectileSpeed = 10f;

    [Header("Moving")]

    public float rotateSpeed;

    [Header("Skill Settings")]

    public BossBehaviourManager.Behaviour startBehaviour;
    public BossBehaviourManager.Behaviour[] skillTypesArray;
    public EnemySkillSettings[] skillSettingsArray;
}
