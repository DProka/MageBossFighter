
using UnityEngine;

public class BossBehaviourEvenOddAttack : IBossBehaviour
{
    private BossScript unit;
    private float attackTimer;
    private bool isEven;
    private int attackCounter;

    public BossBehaviourEvenOddAttack(BossScript thisUnit)
    {
        unit = thisUnit;
        isEven = false;
        attackCounter = 2;
    }

    public void Enter()
    {
        attackTimer = unit._settings.evenOddAttackSpeed;
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
    }

    void ShootRound()
    {
        MovePointPrefabScript[] points = GameController.Instance.points;

        for (int i = isEven ? 0 : 1; i < points.Length; i += 2)
        {
            GameController.Instance.InstantiateProjectile(unit._shootPoint.position, points[i].transform.position, false);
        }

        isEven = !isEven;

        attackTimer = unit._settings.evenOddAttackSpeed;
        unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Attack);
        Rotate();

        attackCounter--;
        if (attackCounter <= 0)
            unit.SetRandomBehaviour();
    }

    public void Rotate()
    {
        Vector3 direction = (GameController.Instance.points[0].transform.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit._settings.rotateSpeed);
    }
}
