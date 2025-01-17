
using UnityEngine;

public class BossBehaviourSpiralAttack : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;
    private int nextPointNum = 0;
    int attackCounter = 0;
    private bool clockwise;

    public BossBehaviourSpiralAttack(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;

        clockwise = false;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;

        nextPointNum = 0;
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
        unit.SpawnProjectile(ArenaManager.Instance.GetMovePointPositionByNum(nextPointNum));
        attackTimer = settings.attackSpeed;

        int nextNum = clockwise ? 1 : -1;
        nextPointNum += nextNum;

        CheckNextNum();
        UpdateCounter();
    }

    private void CheckNextNum()
    {
        if (nextPointNum >= 12)
            nextPointNum = 0;
        
        else if (nextPointNum < 0)
            nextPointNum = 11;
    }

    private void UpdateCounter()
    {
        if (attackCounter < 1)
            unit.SetRandomBehaviour();
        
        else
            attackCounter--;
    }

    private void Rotate()
    {
        Vector3 direction = (ArenaManager.Instance.GetMovePointPositionByNum(nextPointNum) - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
