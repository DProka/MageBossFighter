
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ProjectileManager manager;

    private UnitGeneral target;
    private Vector3 targetPos;

    private bool isPlayer;
    private float damage;
    private float speed;
    private float lifeTimer;

    public void Init(ProjectileManager _manager, Vector3 _targetPos, bool _isPlayer, float _damage, float _speed)
    {
        GameEventBus.OnSomeoneDies += DestroyBullet;

        manager = _manager;
        targetPos = _targetPos;
        isPlayer = _isPlayer;
        target = _isPlayer ? GameController.Instance.enemy : GameController.Instance.player;
        damage = _damage;
        speed = _speed;
        lifeTimer = 5f;

        Vector3 direction = (targetPos - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void UpdateProjectile()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        lifeTimer -= Time.deltaTime;

        Vector3 projectilePos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);

        float distance = Vector3.Distance(targetPos, projectilePos);

        if (distance < 1.2f)
        {
            target.GetHit(damage);

            if (isPlayer)
                GameController.Instance.player.GetComboPoint();

            DestroyBullet();
        }
        else if (lifeTimer <= 0)
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        StartVfx();

        manager.RemoveProjectile(this);
        Destroy(gameObject);
    }

    private void StartVfx()
    {
        if (isPlayer)
        {
            if (GameController.Instance.player.comboSkill1IsActive)
                VfxManager.Instance.StartExplosion(transform.position, true);
            else
                VfxManager.Instance.StartExplosion(transform.position, false);
        }
    }

    private void OnDestroy()
    {
        GameEventBus.OnSomeoneDies -= DestroyBullet;
    }
}
