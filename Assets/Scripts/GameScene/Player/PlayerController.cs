using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Main")]

    public float health;
    public HealthBar healthBar;
    public Animator animator;
    public bool isAlive;
    private float currentHealth;
    public PlayerMovement movementScript;
    public bool isMoving;

    [Header("Attack")]

    public EnemyController enemy;
    public Transform shootPoint;
    public Projectile bulletPrefab;

    public PlayerShooting shootingScript;
    //public float damage;
    //public float idleShootCooldown;
    //public float shootCooldown;
    //public float shootTimer;

    //public bool prepShoot;
    //public float timeBeforeShoot;
    //public float timerBeforeShoot;

    [Header("Stasuses")]

    public PlayerStatus burnStatus;
    public PlayerStatus freezeStatus;

    public float timeToBurn;
    public float timeBeforeFreezeDamage;
    public float freezeStatusTime;

    private float burnTimer;
    private float timeTofreezeDamage;
    private float freezeStatusTimer;

    public void Init()
    {
        enemy = GameController.Instance.enemy;
        movementScript = gameObject.GetComponent<PlayerMovement>();
        movementScript.Init();
        shootingScript = gameObject.GetComponent<PlayerShooting>();
        shootingScript.Init();
        RestartPlayer();
    }

    public void PlayerUpdate()
    {
        if (isAlive)
        {
            CheckStatus();

            movementScript.UpdateMovement();
            shootingScript.UpdateShooting();
            //if (shootTimer > 0)
            //    shootTimer -= Time.deltaTime;

            //else
            //{
            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {
            //        Shoot();
            //        shootTimer = shootCooldown;
            //    }
            //}
        }
        if (currentHealth <= 0 && isAlive)
        {
            PlayDeathAnimation();
        }
    }

    //public void Shoot()
    //{
    //    PlayerBullet bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
    //    bullet.SetBulletActive(shootPoint.position, enemy.transform.position);
    //    bullet.damage = damage;
    //    StartCoroutine(PlayShootAnimation());
    //}

    public void GetHit(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            StartCoroutine(PlayGetHitAnimation());
        }
        healthBar.SetHealth(currentHealth);
    }

    //Statuses__________________________________________________________________________

    void CheckStatus()
    {
        if (burnStatus == PlayerStatus.NoStatus && freezeStatus == PlayerStatus.NoStatus)
            return;
        else
        {
            CheckBurnStatus();
            CheckFreezeStatus();
        }
    }

    void CheckBurnStatus()
    {
        if (burnStatus == PlayerStatus.NoStatus)
        {
            burnTimer = timeToBurn;
        }

        if (burnStatus == PlayerStatus.Burn)
        {
            if (burnTimer > 0)
            {
                burnTimer -= Time.deltaTime;
            }
            else
            {
                GetHit(enemy.burnDamage);
                burnTimer = timeToBurn;
                burnStatus = PlayerStatus.NoStatus;
            }
        }
    }

    void CheckFreezeStatus()
    {
        if (freezeStatus == PlayerStatus.NoStatus)
        {
            timeTofreezeDamage = timeBeforeFreezeDamage;
        }

        if (freezeStatus == PlayerStatus.Freeze)
        {
            if (timeTofreezeDamage > 0)
            {
                timeTofreezeDamage -= Time.deltaTime;
            }
            else
            {
                GetHit(enemy.freezeDamage);
                timeTofreezeDamage = timeBeforeFreezeDamage;
                freezeStatusTimer = enemy.freezeStatusTimer;
                freezeStatus = PlayerStatus.NoStatus;
            }
        }

        if (freezeStatusTimer > 0)
        {
            freezeStatusTimer -= Time.deltaTime;

            if(shootingScript.shootCooldown == shootingScript.idleShootCooldown)
                shootingScript.shootCooldown *= enemy.powerOfFreeze;
        }
        else
            shootingScript.shootCooldown = shootingScript.idleShootCooldown;
    }

    public enum PlayerStatus
    {
        NoStatus,
        Burn,
        Freeze
    }

    //Animations______________________________________________________________

    public IEnumerator PlayStepAnimation(bool isLeft)
    {
        if (isLeft)
        {
            animator.SetBool("Left", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("Left", false);
        }
        else
        {
            animator.SetBool("Right", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("Right", false);
        }
    }

    public IEnumerator PlayShootAnimation()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Attack", false);
    }

    public IEnumerator PlayGetHitAnimation()
    {
        animator.SetBool("GetHit", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("GetHit", false);
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("Death", true);
        isAlive = false;
    }

    //___________________________________________________________________________________

    public void RestartPlayer()
    {
        animator.SetBool("Death", false);
        isAlive = true;
        movementScript.moveTimer = 0;
        movementScript.isMoving = false;
        currentHealth = health;
        healthBar.SetMaxHealth(currentHealth);
        shootingScript.shootCooldown = shootingScript.idleShootCooldown;
        shootingScript.shootTimer = shootingScript.shootCooldown;
        shootingScript.timerBeforeShoot = 0;
    }
}
