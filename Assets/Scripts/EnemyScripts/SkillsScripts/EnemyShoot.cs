
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private EnemyController controller;

    [SerializeField] float damage;
    [SerializeField] float shootSpeed;
    private float shootTimer;

    private PlayerController target;
    private Transform shootPoint;
    private EnemyBullet bulletPrefab;

    private void Start()
    {
        shootTimer = shootSpeed;
        controller = GetComponent<EnemyController>();
        controller.UpdEnemy.AddListener(SkillUpdate);
        target = controller.GetPlayer();
        shootPoint = controller.GetShootPoint();
        bulletPrefab = controller.GetProjectile();
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
        EnemyBullet bullet = Instantiate(bulletPrefab, controller.shootPoint.position, Quaternion.identity);
        bullet.SetBulletActive(controller.shootPoint.position, controller.player.movementScript.targetPoint.transform.position);
        bullet.damage = damage;
        StartCoroutine(controller.PlayThrowAnimation());
    }
}
