
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
            ShootSector();
    }

    private void ShootSector()
    {
        attackTimer = settings.attackSpeed;

        MovePointPrefabScript[] points = GameController.Instance.movePointsArray;

        if (attackCounter == 0)
        {
            SetPointNumbers();
            unit.SpawnProjectile(points[startNum].transform.position);
        }
        else
        {
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
        int[] sectorPoint = ArenaSupportScript.GetThreePointSector();
        leftPoints = new int[] { sectorPoint[1], sectorPoint[0] };
        rightPoints = new int[] { sectorPoint[3], sectorPoint[4] };
    }

    private void Rotate()
    {
        Vector3 direction = (GameController.Instance.movePointsArray[startNum].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
