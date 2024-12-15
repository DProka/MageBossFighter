
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
    private PlayerShooting shootingScript;
    private PlayerStatus statusScript;
    private BossScript enemy;

    public void Init(BossScript _enemy)
    {
        enemy = _enemy;
        movementScript = new PlayerMovement(this, settings, enemy.transform);
        shootingScript = new PlayerShooting(this, settings, shootPoint);
        playerAnimator = new PlayerAnimator(animator);
        statusScript = new PlayerStatus(this, settings);

        ResetPlayer();
    }

    public void PlayerUpdate()
    {
        if (isAlive)
        {
            movementScript.UpdateScript();
            shootingScript.UpdateScript();
            statusScript.UpdateScript();
        }
    }

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
        }
    }

    public void SpawnProjectile() => GameController.Instance.InstantiatePlayerProjectile(shootPoint.position);
    
    public void CheckWaypointStatus()
    {
        MovePointPrefabScript.Status pointStatus = movementScript.targetPoint.pointStatus;
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
        isAlive = true;
        
        float healthMultiplier = (settings.maxHealth / 100) * DataHolder.statsLvls[0];
        currentHealth = Mathf.Round(settings.maxHealth + healthMultiplier);

        UIController.Instance.UpdateHealthBar(true, settings.maxHealth, currentHealth);
        movementScript.ResetWayPoint();
        playerAnimator.StartAnimation(PlayerAnimator.Clip.Idle);
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
        if(!isMoving)
            shootingScript.UIAttack(); 
    }
    
    public void MovePlayer(float direction)
    {
        if(direction > 0)
            MoveRight();
        
        else
            MoveLeft();
    }

    public void MoveLeft() { movementScript.UIChangePointByButton(true); }

    public void MoveRight() { movementScript.UIChangePointByButton(false); }

    #endregion
}
