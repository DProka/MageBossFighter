
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float damage;
    public float idleShootCooldown;
    public float shootCooldown;
    public float shootTimer;

    public bool prepShoot;
    public float timeBeforeShoot;
    public float timerBeforeShoot;

    private PlayerController playerController;

    public void Init()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    public void UpdateShooting()
    {
        if (shootTimer > 0)
            shootTimer -= Time.deltaTime;

        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                shootTimer = shootCooldown;
            }
        }
    }

    public void Shoot()
    {
        //PlayerBullet bullet = Instantiate(playerController.bulletPrefab, playerController.shootPoint.position, Quaternion.identity);
        //bullet.SetBulletActive(playerController.shootPoint.position, playerController.enemy.transform.position);
        //bullet.damage = damage;

        GameController.Instance.InstantiateProjectile(playerController.shootPoint.position, true);

        StartCoroutine(playerController.PlayShootAnimation());
    }
}
