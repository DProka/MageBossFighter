using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileSettings settings;
    [SerializeField] MeshRenderer meshRenderer;
    private ProjectileManager manager;

    private UnitGeneral target;
    
    private float lifeTimer;

    public void Init(ProjectileManager _manager, UnitGeneral _target)
    {
        manager = _manager;
        target = _target;
        lifeTimer = settings.lifeTime;
        meshRenderer.material = settings.material;
    }

    public void UpdateProjectile()
    {
        transform.position += transform.forward * Time.deltaTime * settings.bulletSpeed;
        //transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        lifeTimer -= Time.deltaTime;

        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance < 1.5f)
        {
            target.GetHit(settings.damage);
            DestroyBullet();
        }
        else if (lifeTimer <= 0)
        {
            DestroyBullet();
        }
    }

    public void SetBulletActive(Vector3 startPosition, Vector3 targetPosition)
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void DestroyBullet()
    {
        manager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}
