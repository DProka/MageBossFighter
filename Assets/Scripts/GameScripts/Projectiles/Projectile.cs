using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    private ProjectileManager manager;
    private ProjectileSettings settings;

    private UnitGeneral target;
    private Vector3 targetPos;

    private float damage;
    private float lifeTimer;

    public void Init(ProjectileManager _manager, Vector3 _targetPos, ProjectileSettings _settings, bool isPlayer, float _damage)
    {
        GameEventBus.OnSomeoneDies += DestroyBullet;

        manager = _manager;
        targetPos = _targetPos;
        target = isPlayer ? GameController.Instance.enemy : GameController.Instance.player;
        settings = _settings;

        damage = _damage;
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

        //float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 projectilePos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);

        float distance = Vector3.Distance(targetPos, projectilePos);

        if (distance < 0.8f)
        {
            target.GetHit(damage);
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

    private void OnDestroy()
    {
        GameEventBus.OnSomeoneDies -= DestroyBullet;
    }
}
