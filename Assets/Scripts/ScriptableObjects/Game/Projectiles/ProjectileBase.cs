
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileBase", menuName = "ScriptableObject/Game/ProjectileBase")]
public class ProjectileBase : ScriptableObject
{
    public Projectile[] projectilePrefabArray;
    public ProjectilePoint[] projectilePointPrefabArray;
}
