
using UnityEngine;

public class BossBehaviourQuadrupleSpiralAttack : IBossBehaviour
{
    private BossScript unit;
    private EnemySkillSettings settings;

    private float attackTimer;
    private int newPoint = 0;
    //private int frontPointNum = 0;
    //private int leftPointNum = 0;
    //private int rightPointNum = 0;
    //private int backPointNum = 0;
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

        newPoint = 0;
        targetNums = new int[] { 0, 3, 6, 9 };

        //frontPointNum = 0;
        //leftPointNum = 2;
        //rightPointNum = 8;
        //backPointNum = 5;
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
        //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, GameController.Instance.points[frontPointNum].transform.position, false);
        //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, GameController.Instance.points[leftPointNum].transform.position, false);
        //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, GameController.Instance.points[rightPointNum].transform.position, false);
        //GameController.Instance.InstantiateProjectile(unit._shootPoint.position, GameController.Instance.points[backPointNum].transform.position, false);
        //unit.SpawnProjectile(GameController.Instance.movePointsArray[frontPointNum].transform.position);
        //unit.SpawnProjectile(GameController.Instance.movePointsArray[leftPointNum].transform.position);
        //unit.SpawnProjectile(GameController.Instance.movePointsArray[rightPointNum].transform.position);
        //unit.SpawnProjectile(GameController.Instance.movePointsArray[backPointNum].transform.position);

        //attackTimer = settings.attackSpeed;

        //int nextNum = clockwise ? 1 : -1;
        //frontPointNum += nextNum; 
        //leftPointNum += nextNum;
        //rightPointNum += nextNum;
        //backPointNum += nextNum;

        ////unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);

        //frontPointNum = CheckPointNum(frontPointNum);
        //leftPointNum = CheckPointNum(leftPointNum);
        //rightPointNum = CheckPointNum(rightPointNum);
        //backPointNum = CheckPointNum(backPointNum);

        //int[] newNums = new int[] { newPoint, newPoint + 3, newPoint + 6, newPoint + 9 };

        for (int i = 0; i < targetNums.Length; i++)
        {
            targetNums[i] = MovePointsSupport.CheckNum(targetNums[i], 0);
            unit.SpawnProjectile(GameController.Instance.movePointsArray[targetNums[i]].transform.position);

            targetNums[i] += clockwise ? 1 : -1;
            targetNums[i] = MovePointsSupport.CheckNextNum(targetNums[i]);
        }

        //newPoint += clockwise ? 1 : -1;
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

    ////private int CheckPointNum(int pointNum)
    ////{
    ////    if (pointNum >= GameController.Instance.movePointsArray.Length)
    ////        pointNum = 0;

    ////    else if (pointNum < 0)
    ////        pointNum = 11;

    ////    return pointNum;
    ////}

    private void Rotate()
    {
        //Vector3 direction = (GameController.Instance.movePointsArray[frontPointNum].transform.position - unit.transform.position).normalized;
        Vector3 direction = GameController.Instance.movePointsArray[0].transform.position.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
