
using UnityEngine;

public class CircleShoot : MonoBehaviour
{
    public BossController enemy;
    public float damage;
    public float shootSpeed;
    public Transform shootPoint;
    public EnemyBullet bulletPrefab;
    
    private float shootTimer;

    private void Start()
    {
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
        for (int i = 0; i < GameController.Instance.points.Length; i++)
        {
            Vector3 pos = GameController.Instance.points[i].transform.position;
            EnemyBullet bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(pos));
            bullet.SetBulletActive(shootPoint.position, pos);
            bullet.damage = damage;
        }
    }
}
