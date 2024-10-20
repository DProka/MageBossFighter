
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float damage;
    public float shootSpeed;
    public PlayerController target;
    public Transform shootPoint;
    public EnemyBullet bulletPrefab;

    private BossController enemy;
    private float shootTimer;

    private void Start()
    {
        shootTimer = shootSpeed;
        enemy = gameObject.GetComponent<BossController>();
        enemy.UpdEnemy.AddListener(SkillUpdate);
    }

    public void SkillUpdate()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootSpeed;
        }
    }

    void Shoot()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab, enemy.shootPoint.position, Quaternion.identity);
        bullet.SetBulletActive(enemy.shootPoint.position, enemy.target.movementScript.targetPoint.transform.position);
        bullet.damage = damage;
        StartCoroutine(enemy.PlayThrowAnimation());
    }
}
