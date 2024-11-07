
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileManagerSettings", menuName = "ScriptableObject/Game/ProjectileManagerSettings")]
public class ProjectileManagerSettings : ScriptableObject
{
    public ProjectileBase playerBase;
    public ProjectileBase enemyBase;
}
