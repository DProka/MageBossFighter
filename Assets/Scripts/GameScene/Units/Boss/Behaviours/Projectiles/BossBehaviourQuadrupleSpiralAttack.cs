
using UnityEngine;

public class BossBehaviourQuadrupleSpiralAttack : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;
    private int attackCounter = 0;
    private int[] targetNums;

    private bool clockwise;

    public BossBehaviourQuadrupleSpiralAttack(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;

        clockwise = false;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;

        targetNums = new int[] { 0, 3, 6, 9 };
    }

    public void Exit()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;

        clockwise = !clockwise;
    }

    public void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else
            Attack();

        Rotate();
    }

    private void Attack()
    {
        for (int i = 0; i < targetNums.Length; i++)
        {
            //targetNums[i] = ArenaSupportScript.CheckNum(targetNums[i], 0);
            targetNums[i] = ArenaSupportScript.CheckNum(targetNums[i]);
            unit.SpawnProjectile(GameController.Instance.movePointsArray[targetNums[i]].transform.position);

            targetNums[i] += clockwise ? 1 : -1;
            targetNums[i] = ArenaSupportScript.CheckNum(targetNums[i]);
        }

        unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);
        attackTimer = settings.attackSpeed;

        UpdateAttackCounter();
    }

    private void UpdateAttackCounter()
    {
        attackCounter--;
        if (attackCounter <= 0)
            unit.SetRandomBehaviour();
    }

    private void Rotate()
    {
        //Vector3 direction = (GameController.Instance.movePointsArray[frontPointNum].transform.position - unit.transform.position).normalized;
        Vector3 direction = GameController.Instance.movePointsArray[0].transform.position.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
