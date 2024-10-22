
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    public float damage;

    private BossController target;
    private PlayerScript player;
    private float lifeTimer;

    void Start()
    {
        target = GameController.Instance.enemy;
        player = GameController.Instance.player;
        //damage = player.shootingScript.damage;
        lifeTimer = lifeTime;
        GameController.Instance.playerBullets.Add(this);
    }

    public void UpdateBullet()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
        lifeTimer -= Time.deltaTime;

        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance < 1.5f)
        {
            target.GetHit(damage);
            DestroyBullet();
        }
        else if (lifeTimer < 0)
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
        GameController.Instance.playerBullets.Remove(this);
        Destroy(gameObject);
    }
}
