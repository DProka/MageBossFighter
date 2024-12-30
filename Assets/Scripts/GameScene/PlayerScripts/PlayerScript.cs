
using UnityEngine;

public class PlayerScript : UnitGeneral
{
    public PlayerAnimator playerAnimator { get; private set; }
    public bool isAlive { get; private set; }
    public int currentPointNum { get; private set; }
    public float _currentHealth => currentHealth;

    public PlayerSettings _settings => settings;
    public bool isMoving => movementScript.isMoving;
    public bool isFreeze => statusScript.isFreeze;

    [Header("Main")]

    [SerializeField] PlayerSettings settings;
    [SerializeField] Animator animator;
    [SerializeField] Transform shootPoint;

    private PlayerMovement movementScript;
    private PlayerAttack attackScript;
    private PlayerStatus statusScript;
    private BossScript enemy;

    private bool movementIsClockwise;

    private float damage;
    private int damageComboPoints;

    public void Init(BossScript _enemy)
    {
        enemy = _enemy;
        movementScript = new PlayerMovement(this, settings, enemy.transform);
        attackScript = new PlayerAttack(this);
        playerAnimator = new PlayerAnimator(animator);
        statusScript = new PlayerStatus(this, settings);

        movementIsClockwise = true;

        ResetPlayer();
    }

    public void PlayerUpdate()
    {
        if (isAlive)
        {
            movementScript.UpdateScript();
            attackScript.UpdateScript();
            statusScript.UpdateScript();
        }
    }

    #region Health

    public override void GetHit(float damage)
    {
        if (isAlive)
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
            ResetComboPoints();
        }
    }

    #endregion

    #region Attack

    public void SpawnProjectile()
    {
        float comboMultiplier = (damage / 100) * damageComboPoints;
        float statsMultiplier = (damage / 100) * DataHolder.statsLvls[1];
        float finalDamage = Mathf.Round(damage + statsMultiplier + comboMultiplier);

        GameController.Instance.InstantiatePlayerProjectile(shootPoint.position, finalDamage);
    }

    public float GetAttackSpeed()
    {
        float speedMultiplier = (settings.attackDelay / 100) * DataHolder.statsLvls[2];
        float attackSpeed = settings.attackDelay - speedMultiplier;
        float attackDelay = isFreeze ? (attackSpeed * settings.freezeSpeedFactor) : attackSpeed;

        return attackDelay;
    }

    public void GetComboPoint()
    {
        if(damageComboPoints < 50)
        {
            damageComboPoints += 1;
            UIController.Instance.UpdateComboBar(50, damageComboPoints);
        }
    }

    public void ResetComboPoints()
    {
        damageComboPoints = 0;
        UIController.Instance.UpdateComboBar(50, 0);
    }

    #endregion

    public void CheckWaypointStatus()
    {
        MovePointPrefabScript.Status pointStatus = movementScript.targetPoint.currentStatus;
        currentPointNum = movementScript.targetPoint.id;

        switch (pointStatus)
        {
            case MovePointPrefabScript.Status.NoStatus:
                //movementScript.targetPoint.SetNewStatus(MovePointPrefabScript.Status.Player);
                break;

            case MovePointPrefabScript.Status.Burn:
                statusScript.SetStatus(PlayerStatus.Status.Burn);
                break;

            case MovePointPrefabScript.Status.Freeze:
                statusScript.SetStatus(PlayerStatus.Status.Freeze);
                break;
        }
    }

    public void ResetPlayer()
    {
        ResetStats();

        isAlive = true;

        float healthMultiplier = (settings.maxHealth / 100) * DataHolder.statsLvls[0];
        currentHealth = Mathf.Round(settings.maxHealth + healthMultiplier);

        UIController.Instance.UpdateHealthBar(true, settings.maxHealth, currentHealth);
        movementScript.ResetWayPoint();
        playerAnimator.StartAnimation(PlayerAnimator.Clip.Idle);
    }

    private void ResetStats()
    {
        damage = settings.damage;

        
    }

    private void SetDead()
    {
        isAlive = false;
        playerAnimator.StartAnimation(PlayerAnimator.Clip.Death);
        GameEventBus.OnSomeoneDies?.Invoke();
    }

    #region Controls

    public void Attack()
    {
        if (!isMoving)
            attackScript.UIAttack();
    }

    public void MovePlayer(float direction)
    {
        if (direction > 0)
            MoveRight();

        else
            MoveLeft();
    }

    public void MoveLeft() { movementScript.UIChangePointByButton(true); }

    public void MoveRight() { movementScript.UIChangePointByButton(false); }

    public void CheckMovePoint()
    {
        if (movementScript.lastPoint > 3 && movementScript.lastPoint < 9)
            movementIsClockwise = false;
        else
            movementIsClockwise = true;
    }

    #endregion
}
