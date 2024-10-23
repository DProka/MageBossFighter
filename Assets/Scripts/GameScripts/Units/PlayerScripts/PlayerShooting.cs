
using UnityEngine;

public class PlayerShooting
{
    private PlayerScript controller;
    private PlayerSettings settings;
    private Transform shootPoint;
    private bool canShoot;

    private float attackTimer;

    public PlayerShooting(PlayerScript playerController, PlayerSettings playerSettings, Transform _shootPoint)
    {
        controller = playerController;
        settings = playerSettings;
        shootPoint = _shootPoint;
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
                //UpdateMouse();
                UpdateKeys();
            }
        }
    }

    public void Shoot()
    {
        attackTimer = settings.attackDelay;
        GameController.Instance.InstantiateProjectile(shootPoint.position, true);
        controller.playerAnimator.StartAnimation(PlayerAnimator.Clip.Attack);
    }

    public void SetCanShoot(bool _canShoot) => canShoot = _canShoot;

    private void UpdateMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void UpdateKeys()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }
}
