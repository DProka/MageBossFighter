using System.Collections;
using UnityEngine;

public class SpiralShoot : MonoBehaviour
{
    public BossScript enemy;
    public float damage;
    public float shootSpeed;
    public float timeBetweenBullets;
    public Transform shootPoint;
    public EnemyBullet bulletPrefab;

    private float shootTimer;

    private void Start()
    {
        //enemy.UpdEnemy.AddListener(SkillUpdate);
    }

    void SkillUpdate()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            StartCoroutine(Shoot());
            shootTimer = shootSpeed;
        }
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < GameController.Instance.points.Length; i++)
        {
            Vector3 pos = GameController.Instance.points[i].transform.position;
            EnemyBullet bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(pos));
            bullet.SetBulletActive(shootPoint.position, pos);
            bullet.damage = damage;
            yield return new WaitForSeconds(timeBetweenBullets);
        }
    }
}
