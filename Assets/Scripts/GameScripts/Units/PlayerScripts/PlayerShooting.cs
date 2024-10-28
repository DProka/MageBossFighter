
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
            attackTimer -= Time.deltaTime;

        else
        {
            if (!player.isMoving)
            {
                //UpdateMouse();
                UpdateKeys();
            }
        }
    }

    public void SetCanShoot(bool _canShoot) => canShoot = _canShoot;

    public void UIAttack()
    {
        if (attackTimer > 0)
            return;

        float attackDelay = player.isFreeze ? (settings.attackDelay * settings.freezeSpeedFactor) : settings.attackDelay;
        attackTimer = attackDelay;
        //attackTimer = settings.attackDelay;
        //GameController.Instance.InstantiateProjectile(shootPoint.position, GameController.Instance.enemy.transform.position, true);
        player.SpawnProjectile(GameController.Instance.enemy.transform.position);

        player.playerAnimator.StartAnimation(PlayerAnimator.Clip.Attack);
    }
    
    private void Attack()
    {
        float attackDelay = player.isFreeze ? (settings.attackDelay * settings.freezeSpeedFactor) : settings.attackDelay;
        attackTimer = attackDelay;
        //attackTimer = settings.attackDelay;
        //GameController.Instance.InstantiateProjectile(shootPoint.position, GameController.Instance.enemy.transform.position, true);
        player.SpawnProjectile(GameController.Instance.enemy.transform.position);
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
