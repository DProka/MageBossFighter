
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //private float damage;
    //private float bulletSpeed;
    //private float lifeTime;

    private ProjectileSettings settings;
    private Transform owner;
    private Transform target;
    private float timerToDeath;

    private ProjectilesManager manager;

    public void Init(ProjectilesManager _manager, ProjectileSettings _settings, Transform _owner, Transform _target)
    {
        manager = _manager;
        owner = _owner;
        target = _target;
        timerToDeath = settings.lifeTime;

        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void UpdateProjectile()
    {
        transform.position += transform.forward * Time.deltaTime * settings.bulletSpeed;
        //transform.position = new Vector3(transform.position.x, target.shootPoint.transform.position.y, transform.position.z);

        timerToDeath -= Time.deltaTime;

        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance < 1.5f)
        {
            //owner.GetHit(damage);
            DestroyBullet();
        }
        else if (timerToDeath <= 0)
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        manager.onProjectileDeleted?.Invoke(this);
        Destroy(gameObject);
    }
}
