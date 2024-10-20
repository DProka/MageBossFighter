
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public float bulletSpeed;
    public float lifeTime;

    private PlayerController player;
    private EnemyController enemy;
    private float lifeTimer;
    
    void Start()
    {
        player = GameController.gameController.player;
        enemy = GameController.gameController.enemy;
        damage = enemy.damage;
        lifeTimer = lifeTime;
        GameController.gameController.enemyBullets.Add(this);
    }

    public void UpdateBullet()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
        transform.position = new Vector3(transform.position.x, enemy.shootPoint.transform.position.y, transform.position.z);

        lifeTimer -= Time.deltaTime;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 1.5f)
        {
            player.GetHit(damage);
            DestroyBullet();
        }
        else if (lifeTimer <= 0)
        {
            DestroyBullet();
        }
    }

    public void SetBulletActive(Vector3 startPosition, Vector3 targetPosition)
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void DestroyBullet()
    {
        GameController.gameController.enemyBullets.Remove(this);
        Destroy(gameObject);
    }
}
