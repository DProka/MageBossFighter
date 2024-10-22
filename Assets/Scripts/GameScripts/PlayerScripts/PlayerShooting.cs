
using UnityEngine;

public class PlayerShooting
{
    private PlayerScript controller;
    private PlayerSettings settings;
    private bool canShoot;

    private float attackTimer;

    public PlayerShooting(PlayerScript playerController, PlayerSettings playerSettings)
    {
        controller = playerController;
        settings = playerSettings;
        canShoot = true;
    }

    public void UpdateScript()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            if (!controller.isMoving)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
            }
        }
    }

    public void Shoot()
    {
        attackTimer = settings.attackDelay;
        GameController.Instance.InstantiateProjectile(controller.shootPoint.position, true);
        controller.playerAnimator.StartAnimation(PlayerAnimator.Clip.Attack);
    }

    public void SetCanShoot(bool _canShoot) => canShoot = _canShoot;
}
