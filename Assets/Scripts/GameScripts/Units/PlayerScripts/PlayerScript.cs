
using UnityEngine;

public class PlayerScript : UnitGeneral
{
    public PlayerAnimator playerAnimator { get; private set; }

    public bool isAlive { get; private set; }
    public bool isMoving => movementScript.isMoving;

    [Header("Main")]

    [SerializeField] PlayerSettings settings;
    [SerializeField] Animator animator;
    [SerializeField] Transform shootPoint;

    //public PlayerStatus burnStatus;
    //public PlayerStatus freezeStatus;

    //public float timeToBurn;
    //public float timeBeforeFreezeDamage;
    //public float freezeStatusTime;

    //private float burnTimer;
    //private float timeTofreezeDamage;
    //private float freezeStatusTimer;

    private PlayerMovement movementScript;
    private PlayerShooting shootingScript;
    private BossScript enemy;

    public void Init(BossScript _enemy)
    {
        enemy = _enemy;
        movementScript = new PlayerMovement(this, settings, enemy.transform);
        shootingScript = new PlayerShooting(this, settings, shootPoint);
        playerAnimator = new PlayerAnimator(animator);

        ResetPlayer();
    }

    public void PlayerUpdate()
    {
        if (isAlive)
        {
            //CheckStatus();

            movementScript.UpdateMovement();
            shootingScript.UpdateScript();
        }
    }

    public override void GetHit(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            playerAnimator.StartAnimation(PlayerAnimator.Clip.GetHit);
        }
        else
        {
            currentHealth = 0;
            SetDead();
        }

        UIController.Instance.UpdateHealthBar(true, settings.maxHealth, currentHealth);
    }

    #region Statuses

    //void CheckStatus()
    //{
    //    if (burnStatus == PlayerStatus.NoStatus && freezeStatus == PlayerStatus.NoStatus)
    //        return;
    //    else
    //    {
    //        CheckBurnStatus();
    //        CheckFreezeStatus();
    //    }
    //}

    //void CheckBurnStatus()
    //{
    //    if (burnStatus == PlayerStatus.NoStatus)
    //    {
    //        burnTimer = timeToBurn;
    //    }

    //    if (burnStatus == PlayerStatus.Burn)
    //    {
    //        if (burnTimer > 0)
    //        {
    //            burnTimer -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            GetHit(enemy.burnDamage);
    //            burnTimer = timeToBurn;
    //            burnStatus = PlayerStatus.NoStatus;
    //        }
    //    }
    //}

    //void CheckFreezeStatus()
    //{
    //    if (freezeStatus == PlayerStatus.NoStatus)
    //    {
    //        timeTofreezeDamage = timeBeforeFreezeDamage;
    //    }

    //    if (freezeStatus == PlayerStatus.Freeze)
    //    {
    //        if (timeTofreezeDamage > 0)
    //        {
    //            timeTofreezeDamage -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            GetHit(enemy.freezeDamage);
    //            timeTofreezeDamage = timeBeforeFreezeDamage;
    //            freezeStatusTimer = enemy.freezeStatusTimer;
    //            freezeStatus = PlayerStatus.NoStatus;
    //        }
    //    }

    //    if (freezeStatusTimer > 0)
    //    {
    //        freezeStatusTimer -= Time.deltaTime;
    //    }
    //}

    //public enum PlayerStatus
    //{
    //    NoStatus,
    //    Burn,
    //    Freeze
    //}

    #endregion

    private void SetDead()
    {
        isAlive = false;
        playerAnimator.StartAnimation(PlayerAnimator.Clip.Death);
        GameEventBus.OnSomeoneDies?.Invoke();
    }
    
    public void ResetPlayer()
    {
        isAlive = true;
        currentHealth = settings.maxHealth;
        UIController.Instance.UpdateHealthBar(true, settings.maxHealth, currentHealth);
        movementScript.ResetWayPoint();
        playerAnimator.StartAnimation(PlayerAnimator.Clip.Idle);
    }
}
