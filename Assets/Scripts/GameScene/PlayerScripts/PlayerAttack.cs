
using UnityEngine;

public class PlayerAttack
{
    private PlayerScript player;
    private bool canShoot;

    private float attackTimer;

    public PlayerAttack(PlayerScript playerController)
    {
        player = playerController;
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
        attackTimer = player.GetAttackSpeed();
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
