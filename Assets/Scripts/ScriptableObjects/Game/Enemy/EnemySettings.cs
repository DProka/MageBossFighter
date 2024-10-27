
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObject/Boss/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [Header("Main")]
    
    public float maxHealth;

    [Header("Skill Settings")]

    public BossBehaviourManager.Behaviour startSkill;
    public BossBehaviourManager.Behaviour[] skillsArray;

    [Header("Attack")]

    public float damage;
    public float delayBeforeAttack = 2f;

    [Header("Skills")]

    public EnemySkillSettingsBase skillBase;

    [Header("Moving")]

    public float rotateSpeed;
}
