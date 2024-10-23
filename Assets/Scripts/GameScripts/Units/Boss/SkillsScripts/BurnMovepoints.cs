
using UnityEngine;

public class BurnMovepoints : MonoBehaviour
{
    public BossScript enemy;
    public float damage;
    public float timeBeforeDamage;
    public float cooldown;

    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = cooldown;
        SetDamage();
        enemy.UpdEnemy.AddListener(SkillUpdate);
    }

    public void SkillUpdate()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        else
        {
            ChoosePoint();
            cooldownTimer = cooldown;
        }
    }

    void ChoosePoint()
    {
        //int random = Random.Range(0, GameController.Instance.points.Length);
        //Status status = GameController.Instance.points[random].pointStatus;
        //if (status != Status.Burning)
        //{
        //    GameController.Instance.points[random].GetNewStatus(Status.Burning);
        //    return;
        //}
        //else
        //{
        //    for (int i = 0; i < GameController.Instance.points.Length; i++)
        //    {
        //        status = GameController.Instance.points[i].pointStatus;
        //        if (status != Status.Burning)
        //        {
        //            GameController.Instance.points[i].GetNewStatus(Status.Burning);
        //            return;
        //        }
        //    }
        //}
    }

    void SetDamage()
    {
        enemy.burnDamage = damage;
        //enemy.target.timeToBurn = timeBeforeDamage;
    }
}
