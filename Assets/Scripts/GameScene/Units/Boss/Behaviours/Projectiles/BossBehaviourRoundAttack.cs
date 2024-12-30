
using UnityEngine;

public class BossBehaviourRoundAttack : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;

    public BossBehaviourRoundAttack(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
    }

    public void Exit()
    {
        attackTimer = unit._settings.delayBeforeAttack;
    }

    public void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else
            FreezePoint();

        //Rotate();
    }

    private void FreezePoint()
    {
        MovePointPrefabScript[] points = GameController.Instance.movePointsArray;

        foreach(MovePointPrefabScript point in points)
        {
            //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, point.transform.position, false);
            unit.SpawnProjectile(point.transform.position);
        }

        attackTimer = settings.attackSpeed;
        unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);
        Rotate();
    }

    private void Rotate()
    {
        Vector3 direction = (GameController.Instance.movePointsArray[0].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
