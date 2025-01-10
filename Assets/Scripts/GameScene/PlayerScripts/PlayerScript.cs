
using UnityEngine;

public class PlayerScript : UnitGeneral
{
    public PlayerAnimator playerAnimator { get; private set; }
    public bool isAlive { get; private set; }
    //public int currentPointNum { get; private set; }

    public bool comboSkill1IsActive { get; private set; }
    public bool comboSkill2IsActive { get; private set; }

    public float _currentHealth => currentHealth;
    public int currentPointNum => movementScript.lastPoint;

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
    private int comboPoints;

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
        float statsMultiplier = (damage / 100) * DataHolder.statsLvls[1];

        float comboMultiplier = 0;
        if (comboSkill1IsActive)
            comboMultiplier = damage / 2;

        float finalDamage = Mathf.Round(damage + statsMultiplier + comboMultiplier);

        GameController.Instance.InstantiatePlayerProjectile(shootPoint.position, finalDamage);
    }

    public float GetAttackSpeed()
    {
        float speedMultiplier = (settings.attackDelay / 100) * DataHolder.statsLvls[2];
        
        float comboMultiplier = 0;
        if (comboSkill2IsActive)
            comboMultiplier = settings.attackDelay / 3;
        
        float attackSpeed = settings.attackDelay - speedMultiplier - comboMultiplier;
        float attackDelay = isFreeze ? (attackSpeed * settings.freezeSpeedFactor) : attackSpeed;

        return attackDelay;
    }

    public void GetComboPoint()
    {
        if (comboPoints < settings.maxComboPoints)
        {
            comboPoints += 1;
            CheckComboSkills();

            UIController.Instance.UpdateComboBar(settings.maxComboPoints, comboPoints);
        }
    }

    public void ResetComboPoints()
    {
        comboPoints = 0;
        CheckComboSkills();

        UIController.Instance.UpdateComboBar(settings.maxComboPoints, 0);
    }

    private void CheckComboSkills()
    {
        comboSkill1IsActive = comboPoints >= settings.maxComboPoints / 2;
        comboSkill2IsActive = comboPoints >= settings.maxComboPoints;
    }

    #endregion

    public void CheckWaypointStatus()
    {
        ArenaManager.PointStatus pointStatus = ArenaManager.Instance.GetMovePointStatusByNum(currentPointNum);
        //ArenaManager.PointStatus pointStatus = movementScript.targetPoint.currentStatus;
        //currentPointNum = movementScript.targetPoint.id;

        switch (pointStatus)
        {
            case ArenaManager.PointStatus.NoStatus:
                //movementScript.targetPoint.SetNewStatus(MovePointPrefabScript.Status.Player);
                break;

            case ArenaManager.PointStatus.Burn:
                statusScript.SetStatus(PlayerStatus.Status.Burn);
                break;

            case ArenaManager.PointStatus.Freeze:
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
