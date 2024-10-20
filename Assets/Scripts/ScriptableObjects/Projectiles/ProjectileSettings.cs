
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSettings", menuName = "ScriptableObject/Game/ProjectileSettings")]
public class ProjectileSettings : MonoBehaviour
{
    public float damage;
    public float bulletSpeed;
    public float lifeTime;
}
