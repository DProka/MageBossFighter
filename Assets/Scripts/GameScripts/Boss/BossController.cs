using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossController : UnitGeneral
{
    public bool isAlive { get; private set; }

    [Header("Main")]

    [SerializeField] EnemySettings settings;

    public PlayerScript target;
    //private float currentHealth;

    private HealthBar healthBar;

    [Header("Animations")]

    public Animator animator;
    private string animState;

    [Header("Skills")]

    public Transform shootPoint;
    public UnityEvent UpdEnemy = new UnityEvent();

    public float burnDamage;
    public float timeToBurn;

    public float freezeDamage;
    public float powerOfFreeze;
    public float timeToFreeze;
    public float freezeStatusTimer;

    public void Init()
    {
        target = GameController.Instance.player;
        healthBar = GameController.Instance.uiScript.bossHB;
        
        ResetEnemy();
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
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * settings.rotateSpeed);

        if (target.movementScript.isMoving)
            SetMovingAnimation(true);
        else
            SetMovingAnimation(false);
    }

    public override void GetHit(float damage)
    {
        if(currentHealth > damage)
        {
            currentHealth -= damage;
            StartCoroutine(PlayGetHitAnimation());
        }
        else
            SetDeath();

        SetHealthBar(true);
    }

    public void ResetEnemy()
    {
        currentHealth = settings.maxHealth;
        SetHealthBar(false);
        animator.SetTrigger("Alive");
        isAlive = true;
    }

    public void SetMovingAnimation(bool isMove)
    {
        if (isMove)
        {
            animator.SetBool("Rotate", true);
        }
        else
        {
            animator.SetBool("Rotate", false);
        }
    }

    public IEnumerator PlayThrowAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Throw", true);
            animator.SetBool("GetHit", false);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("Throw", false);
        }
    }

    IEnumerator PlayGetHitAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("GetHit", true);
            animator.SetBool("Throw", false);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("GetHit", false);
        }
    }

    private void SetDeath()
    {
        currentHealth = 0;
        isAlive = false;

        if (animator != null && isAlive)
            animator.SetTrigger("Death");

        GameEventBus.OnSomeoneDies?.Invoke();
    }

    private void SetHealthBar(bool isHit)
    {
        
        if(isHit)
            healthBar.SetHealth(health, currentHealth);
        else
            healthBar.ResetBar();
    }

}
