
using UnityEngine;

public class BossBehaviourAttackMovepoint : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private int attackCounter = 5;
    private float attackTimer;

    public BossBehaviourAttackMovepoint(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;
    }

    public void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else
        {
            SpawnProjectileOnPoint();
        }

        Rotate();
    }

    public void Exit()
    {

    }

    private void SpawnProjectileOnPoint()
    {
        MovePointPrefabScript targetPoint = GameController.Instance.movePointsArray[unit.target.currentPointNum];
        targetPoint.SetNewStatus(MovePointPrefabScript.Status.Attack);
        //attackTimer = settings.attackSpeed;
        attackTimer = unit._settings.delayBeforeAttack;
        GameController.Instance.InstantiateEnemyPointProjectile(targetPoint.transform.position, 0);
        attackCounter--;

        if (attackCounter <= 0)
            unit.SetRandomBehaviour();
    }

    private void Rotate()
    {
        Vector3 direction = (unit.target.transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
