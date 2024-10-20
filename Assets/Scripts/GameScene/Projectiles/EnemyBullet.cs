
using UnityEngine;

public class EnemyBullet : Projectile
{
    
    
    //void Start()
    //{
    //    owner = GameController.Instance.player;
    //    target = GameController.Instance.enemy;
    //    damage = target.damage;
    //    lifeTimer = lifeTime;
    //    GameController.Instance.enemyBullets.Add(this);
    //}

    //public void UpdateProjectile()
    //{
    //    transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    //    transform.position = new Vector3(transform.position.x, target.shootPoint.transform.position.y, transform.position.z);

    //    lifeTimer -= Time.deltaTime;

    //    float distance = Vector3.Distance(owner.transform.position, transform.position);
    //    if (distance < 1.5f)
    //    {
    //        owner.GetHit(damage);
    //        DestroyBullet();
    //    }
    //    else if (lifeTimer <= 0)
    //    {
    //        DestroyBullet();
    //    }
    //}

    //public void SetBulletActive(Vector3 startPosition, Vector3 targetPosition)
    //{
    //    gameObject.SetActive(true);
    //    transform.position = startPosition;
    //    Vector3 direction = (targetPosition - transform.position).normalized;
    //    direction.y = 0f;
    //    transform.rotation = Quaternion.LookRotation(direction);
    //}

}
