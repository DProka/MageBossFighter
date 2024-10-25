
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObject/Game/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [Header("Main")]
    
    public float maxHealth;

    [Header("Skill Pull")]

    public BossBehaviourManager.Behaviour[] skillsArray;

    [Header("Attack")]

    public float damage;
    public float simpleAttackSpeed;
    public float spiralAttackSpeed;
    public float roundAttackSpeed = 3f;
    public float evenOddAttackSpeed = 3f;

    [Header("Movepoints Attack")]

    public float burnMovepointSpeed = 1f;
    public float freezeMovepointSpeed = 1f;
    public float blockMovepointSpeed = 1f;

    [Header("Moving")]

    public float rotateSpeed;
}
