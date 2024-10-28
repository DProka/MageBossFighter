
using UnityEngine;

public class BossBehaviourSectorAttack : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;

    private int startNum;
    private int[] leftPoints;
    private int[] rightPoints;

    private int attackCounter;

    public BossBehaviourSectorAttack(BossScript thisUnit, EnemySkillSettings _settings)
    {
        unit = thisUnit;
        settings = _settings;
    }

    public void Enter()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;
        //SetPointNumbers();
    }

    public void Exit()
    {
        attackTimer = unit._settings.delayBeforeAttack;
        attackCounter = settings.attackCounter;
    }

    public void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;

            Rotate();
        }
        else
        {
            ShootSector();
        }
    }

    private void ShootSector()
    {
        SetPointNumbers();

        attackTimer = settings.attackSpeed;

        MovePointPrefabScript[] points = GameController.Instance.points;

        if (attackCounter == 0)
            //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, points[startNum].transform.position, false);
            unit.SpawnProjectile(points[startNum].transform.position);
        else
        {
            //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, points[leftPoints[attackCounter - 1]].transform.position, false);
            //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, points[rightPoints[attackCounter - 1]].transform.position, false);
            unit.SpawnProjectile(points[leftPoints[attackCounter - 1]].transform.position);
            unit.SpawnProjectile(points[rightPoints[attackCounter - 1]].transform.position);
        }

        attackCounter++;
        if (attackCounter > 2)
            unit.SetRandomBehaviour();
    }

    private void SetPointNumbers()
    {
        startNum = unit.target.currentPointNum;
        leftPoints = new int[2];
        rightPoints = new int[2];

        for (int i = 0; i < leftPoints.Length; i++)
        {
            int newNum = startNum + i + 1;

            if (newNum >= GameController.Instance.points.Length)
                newNum = 0 + i;

            leftPoints[i] = newNum;
        }

        for (int i = 0; i < rightPoints.Length; i++)
        {
            int newNum = startNum - i - 1;

            if (newNum < 0)
                newNum = GameController.Instance.points.Length - 1 - i;

            rightPoints[i] = newNum;
        }
    }

    private void Rotate()
    {
        Vector3 direction = (GameController.Instance.points[startNum].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
