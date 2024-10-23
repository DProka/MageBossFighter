
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSettings", menuName = "ScriptableObject/Game/ProjectileSettings")]
public class ProjectileSettings : ScriptableObject
{
    public float damage;
    public float bulletSpeed;
    public float lifeTime;
    public Vector3 scale;
    public Material material;
}
