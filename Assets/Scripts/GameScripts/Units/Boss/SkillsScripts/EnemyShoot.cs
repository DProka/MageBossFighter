
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //public float damage;
    //public float shootSpeed;
    //public PlayerScript target;
    //public Transform shootPoint;
    //public EnemyBullet bulletPrefab;

    //private BossScript enemy;
    //private float shootTimer;

    //private void Start()
    //{
    //    shootTimer = shootSpeed;
    //    enemy = gameObject.GetComponent<BossScript>();
    //    enemy.UpdEnemy.AddListener(SkillUpdate);
    //}

    //public void SkillUpdate()
    //{
    //    shootTimer -= Time.deltaTime;
    //    if (shootTimer <= 0)
    //    {
    //        Shoot();
    //        shootTimer = shootSpeed;
    //    }
    //}

    //void Shoot()
    //{
    //    //EnemyBullet bullet = Instantiate(bulletPrefab, enemy.shootPoint.position, Quaternion.identity);
    //    //bullet.SetBulletActive(enemy.shootPoint.position, enemy.target.movementScript.targetPoint.transform.position);
    //    //bullet.damage = damage;

    //    GameController.Instance.InstantiateProjectile(enemy.shootPoint.position, false);

    //    StartCoroutine(enemy.PlayThrowAnimation());
    //}
}
