
using UnityEngine;

public class BossBehaviourDoubleSpiralAttack : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;
    private int frontPointNum = 0;
    private int backPointNum = 0;
    int attackCounter = 0;
    private bool clockwise;

    public BossBehaviourDoubleSpiralAttack(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;

        clockwise = false;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;

        frontPointNum = 0;
        backPointNum = 5;
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
            Shoot();

        Rotate();
    }

    private void Shoot()
    {
        //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, GameController.Instance.points[frontPointNum].transform.position, false);
        //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, GameController.Instance.points[backPointNum].transform.position, false);
        unit.SpawnProjectile(GameController.Instance.points[frontPointNum].transform.position);
        unit.SpawnProjectile(GameController.Instance.points[backPointNum].transform.position);

        attackTimer = settings.attackSpeed;

        int nextNum = clockwise ? 1 : -1;
        frontPointNum += nextNum;
        backPointNum += nextNum;

        //unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);

        frontPointNum = CheckPointNum(frontPointNum);
        backPointNum = CheckPointNum(backPointNum);
        UpdateAttackCounter();
    }

    private void UpdateAttackCounter()
    {
        attackCounter--;
        if (attackCounter <= 0)
            unit.SetRandomBehaviour();
    }

    private int CheckPointNum(int pointNum)
    {
        if (pointNum >= GameController.Instance.points.Length)
            pointNum = 0;
        
        else if (pointNum < 0)
            pointNum = 11;

        return pointNum;
    }

    private void Rotate()
    {
        Vector3 direction = (GameController.Instance.points[frontPointNum].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
