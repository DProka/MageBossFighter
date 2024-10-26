
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObject/Boss/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [Header("Main")]
    
    public float maxHealth;

    [Header("Skill Settings")]

    public BossBehaviourManager.Behaviour[] skillsArray;

    [Header("Attack")]

    public float damage;
    public float delayBeforeAttack = 2f;

    [Header("Skills")]

    public EnemySkillSettingsBase skillBase;

    //public float simpleAttackSpeed;
    public float spiralAttackSpeed;
    public float roundAttackSpeed = 3f;
    public float evenOddAttackSpeed = 3f;
    public float sectorAttackSpeed = 1f;

    [Header("Movepoints Attack")]

    public float burnMovepointSpeed = 1f;
    public float freezeMovepointSpeed = 1f;
    public float blockMovepointSpeed = 1f;

    [Header("Moving")]

    public float rotateSpeed;
}
