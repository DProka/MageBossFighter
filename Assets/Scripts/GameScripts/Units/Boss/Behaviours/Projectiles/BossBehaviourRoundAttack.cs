
using UnityEngine;

public class BossBehaviourRoundAttack : IBossBehaviour
{
    private BossScript unit;
    private float attackTimer;

    public BossBehaviourRoundAttack(BossScript thisUnit)
    {
        unit = thisUnit;
    }

    public void Enter()
    {
        attackTimer = unit._settings.roundAttackSpeed;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else
            ShootRound();

        //Rotate();
    }

    void ShootRound()
    {
        MovePointPrefabScript[] points = GameController.Instance.points;

        foreach(MovePointPrefabScript point in points)
        {
            GameController.Instance.InstantiateProjectile(unit._shootPoint.position, point.transform.position, false);
        }

        attackTimer = unit._settings.roundAttackSpeed;
        unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);
        Rotate();
    }

    public void Rotate()
    {
        Vector3 direction = (GameController.Instance.points[0].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
