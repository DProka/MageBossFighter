
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
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            if (!player.isMoving)
            {
                //UpdateMouse();
                UpdateKeys();
            }
        }
    }

    private void Shoot()
    {
        float attackDelay = player.isFreeze ? (settings.attackDelay * settings.freezeSpeedFactor) : settings.attackDelay;
        attackTimer = attackDelay;
        //attackTimer = settings.attackDelay;
        GameController.Instance.InstantiateProjectile(shootPoint.position, GameController.Instance.enemy.transform.position, true);
        player.playerAnimator.StartAnimation(PlayerAnimator.Clip.Attack);
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
