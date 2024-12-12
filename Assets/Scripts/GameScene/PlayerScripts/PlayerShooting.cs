
using UnityEngine;

public class PlayerShooting
{
    private PlayerScript player;
    private PlayerSettings settings;
    private Transform shootPoint;
    private bool canShoot;

    private float attackTimer;

    public PlayerShooting(PlayerScript playerController, PlayerSettings playerSettings, Transform _shootPoint)
    {
        player = playerController;
        settings = playerSettings;
        shootPoint = _shootPoint;
        canShoot = true;
    }

    public void UpdateScript()
    {
        if (canShoot)
        {
            if (attackTimer > 0)
                attackTimer -= Time.deltaTime;

            else
            {
                if (!player.isMoving)
                {
                    UpdateKeys();
                }
            }
        }
    }

    public void SetCanShoot(bool _canShoot) => canShoot = _canShoot;

    public void UIAttack()
    {
        if (attackTimer > 0)
            return;

        Attack();
    }
    
    private void Attack()
    {
        float speedMultiplier = (settings.attackDelay / 100) * DataHolder.statsLvls[2];
        float attackSpeed = settings.attackDelay - speedMultiplier;
        float attackDelay = player.isFreeze ? (attackSpeed * settings.freezeSpeedFactor) : attackSpeed;
        attackTimer = attackDelay;
        player.SpawnProjectile();
        player.playerAnimator.StartAnimation(PlayerAnimator.Clip.Attack);
    }

    private void UpdateMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void UpdateKeys()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Attack();
        }
    }
}
