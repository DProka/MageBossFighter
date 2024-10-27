
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileBase", menuName = "ScriptableObject/Game/ProjectileBase")]
public class ProjectileBase : ScriptableObject
{
    public Projectile projectilePrefab;
    public ProjectileSettings[] playerProjectilesArray;
    public ProjectileSettings[] enemyProjectilesArray;
}
