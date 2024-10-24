using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourRoundAttack : IBehaviour
{
    private BossScript unit;
    private float attackTimer;
    private int nextPointNum = 0;

    public BossBehaviourRoundAttack(BossScript thisUnit)
    {
        unit = thisUnit;
    }

    public void Enter()
    {
        attackTimer = unit._settings.roundAttackSpeed;
        nextPointNum = 0;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else
            ShootNextPoint();

        Rotate();
    }

    void ShootNextPoint()
    {
        GameController.Instance.InstantiateProjectile(unit._shootPoint.position, GameController.Instance.points[nextPointNum].transform.position, false);
        attackTimer = unit._settings.roundAttackSpeed;
        nextPointNum++;
        //unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);

        if(nextPointNum >= GameController.Instance.points.Length)
            nextPointNum = 0;
    }

    public void Rotate()
    {
        Vector3 direction = (GameController.Instance.points[nextPointNum].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
