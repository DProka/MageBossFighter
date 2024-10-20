using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]

    public float health;
    public float damage;
    public float rotateSpeed;
    
    public bool isAlive;

    [HideInInspector]public PlayerController player;
    private float currentHealth;

    [Header("Enemy Projectiles")]

    [SerializeField] EnemyBullet projectilePrefab;

    [Header("Animations")]

    private Animator animator;
    private string animState;

    [Header("Skills")]

    public Transform shootPoint;
    public UnityEvent UpdEnemy; // = new UnityEvent();

    public float burnDamage;
    public float timeToBurn;

    public float freezeDamage;
    public float powerOfFreeze;
    public float timeToFreeze;
    public float freezeStatusTimer;

    public void Init()
    {
        UpdEnemy = new UnityEvent();
        animator = GetComponent<Animator>();

        player = GameController.gameController.player;
        currentHealth = health;
        SetHealthBar(false);
        isAlive = true;
    }

    public void EnemyUpdate()
    {
        if (isAlive)
        {
            RotateEnemy();

            if (UpdEnemy != null)
                UpdEnemy.Invoke();
        }
    }

    public void RotateEnemy()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    public void GetHit(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
            StartCoroutine(PlayGetHitAnimation());

        else
        {
            currentHealth = 0;
            PlayDeathAnimation();
            isAlive = false;
        }

        SetHealthBar(true);
    }

    //public void SetMovingAnimation(bool isMove)
    //{
    //    if (isMove)
    //    {
    //        animator.SetBool("Rotate", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("Rotate", false);
    //    }
    //}

    public IEnumerator PlayThrowAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Throw", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("Throw", false);
        }
    }

    IEnumerator PlayGetHitAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Throw", false);
            animator.SetBool("Hit", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("Hit", false);
        }
    }

    private void PlayDeathAnimation()
    {
        if (animator != null && isAlive)
        {
            animator.SetTrigger("Death");
        }
    }

    private void SetHealthBar(bool isHit)
    {
        HealthBar healthBar = GameController.gameController.uiScript.bossHB;
        if(isHit)
            healthBar.SetHealth(currentHealth);
        else
            healthBar.SetMaxHealth(currentHealth);
    }

    public void ResetEnemy()
    {
        currentHealth = health;
        SetHealthBar(false);
        animator.ResetTrigger("Death");
        isAlive = true;
    }

    public PlayerController GetPlayer() { return player; }
    public Transform GetShootPoint() { return shootPoint; }
    public EnemyBullet GetProjectile() { return projectilePrefab; }
}
