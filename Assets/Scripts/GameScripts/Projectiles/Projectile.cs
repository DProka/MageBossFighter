using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileSettings settings;
    [SerializeField] MeshRenderer meshRenderer;

    private ProjectileManager manager;

    private UnitGeneral target;
    private Vector3 targetPos;

    private float lifeTimer;

    public void Init(ProjectileManager _manager, Vector3 _targetPos, ProjectileSettings _settings, bool isPlayer)
    {
        manager = _manager;
        targetPos = _targetPos;
        target = isPlayer ? GameController.Instance.enemy : GameController.Instance.player;
        settings = _settings;
        lifeTimer = settings.lifeTime;
        meshRenderer.material = settings.material;
        transform.localScale = settings.scale;

        //Vector3 direction = (_target.transform.position - transform.position).normalized;
        Vector3 direction = (targetPos - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void UpdateProjectile()
    {
        transform.position += transform.forward * Time.deltaTime * settings.bulletSpeed;

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

    public void DestroyBullet()
    {
        manager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}
