
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
        //nextPointNum = clockwise ? 0 : 11;
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
        //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, GameController.Instance.points[nextPointNum].transform.position, false);
        unit.SpawnProjectile(GameController.Instance.points[nextPointNum].transform.position);
        attackTimer = settings.attackSpeed;

        int nextNum = clockwise ? 1 : -1;
        nextPointNum += nextNum;

        CheckNextNum();
        UpdateCounter();
    }

    private void CheckNextNum()
    {
        if (nextPointNum >= GameController.Instance.points.Length)
            nextPointNum = 0;
        
        else if (nextPointNum < 0)
            nextPointNum = 11;
    }

    private void UpdateCounter()
    {
        attackCounter--;
        if (attackCounter <= 0)
            unit.SetRandomBehaviour();
    }

    private void Rotate()
    {
        Vector3 direction = (GameController.Instance.points[nextPointNum].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
