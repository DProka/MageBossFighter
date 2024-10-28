
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObject/Boss/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [Header("Main")]
    
    public float maxHealth;

    [Header("Attack")]

    public float damage;
    public float delayBeforeAttack = 2f;

    [Header("Moving")]

    public float rotateSpeed;

    [Header("Skill Settings")]

    public EnemySkillSettingsBase skillBase;
    public BossBehaviourManager.Behaviour startBehaviour;
    public BossBehaviourManager.Behaviour[] skillsArray;
}
