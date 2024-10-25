
using UnityEngine;

public class BossBehaviourSimpleAttack : IBossBehaviour
{
    private BossScript unit;

    private int attackCounter = 5;
    private float shootTimer;

    public BossBehaviourSimpleAttack(BossScript thisUnit)
    {
        unit = thisUnit;
    }

    public void Enter()
    {
        shootTimer = unit._settings.simpleAttackSpeed;
        attackCounter = 5;
    }

    public void Update()
    {
        if (shootTimer > 0)
            shootTimer -= Time.deltaTime;
        else
            Shoot();

        Rotate();
    }

    public void Exit()
    {

    }

    private void Shoot()
    {
        shootTimer = unit._settings.simpleAttackSpeed;
        //unit.SpawnSimpleProjectile();
        GameController.Instance.InstantiateProjectile(unit._shootPoint.position, unit.target.transform.position, false);
        unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);
        attackCounter--;

        if (attackCounter <= 0)
            unit.SetRandomBehaviour();
    }

    public void Rotate()
    {
        Vector3 direction = (unit.target.transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}