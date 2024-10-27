
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySkillSettingsBase", menuName = "ScriptableObject/Boss/EnemySkillSettingsBase")]
public class EnemySkillSettingsBase : ScriptableObject
{
    [Header("Attack Skills")]

    public EnemySkillSettings simpleAttack;
    public EnemySkillSettings spiralAttack;
    public EnemySkillSettings roundAttack;
    public EnemySkillSettings evenOddAttack;
    public EnemySkillSettings sectorAttack;
    public EnemySkillSettings spiralDoubleAttack;
    public EnemySkillSettings spiralQuadrupleAttack;

    [Header("Movepoint Skills")]

    public EnemySkillSettings burnMovepoint;
    public EnemySkillSettings freezeMovepoint;
    public EnemySkillSettings blockMovepoint;
}
