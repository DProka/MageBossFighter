
using UnityEngine;

public class BossBehaviourEvenOddAttack : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;
    private bool isEven;
    private int attackCounter;

    public BossBehaviourEvenOddAttack(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;

        isEven = false;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;
    }

    public void Exit()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;

        isEven = !isEven;
    }

    public void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else
            ShootRound();
    }

    private void ShootRound()
    {
        MovePointPrefabScript[] points = GameController.Instance.movePointsArray;

        for (int i = isEven ? 0 : 1; i < points.Length; i += 2)
        {
            //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, points[i].transform.position, false);
            unit.SpawnProjectile(points[i].transform.position);
        }

        isEven = !isEven;

        attackTimer = settings.attackSpeed;
        unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);
        Rotate();

        attackCounter--;
        if (attackCounter <= 0)
            unit.SetRandomBehaviour();
    }

    private void Rotate()
    {
        Vector3 direction = (GameController.Instance.movePointsArray[0].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
