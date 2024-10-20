
using UnityEngine;

public class BurnMovepoints : MonoBehaviour
{
    public EnemyController enemy;
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
        int random = Random.Range(0, GameController.gameController.points.Length);
        Status status = GameController.gameController.points[random].pointStatus;
        if (status != Status.Burning)
        {
            GameController.gameController.points[random].GetNewStatus(Status.Burning);
            return;
        }
        else
        {
            for (int i = 0; i < GameController.gameController.points.Length; i++)
            {
                status = GameController.gameController.points[i].pointStatus;
                if (status != Status.Burning)
                {
                    GameController.gameController.points[i].GetNewStatus(Status.Burning);
                    return;
                }
            }
        }
    }

    void SetDamage()
    {
        enemy.burnDamage = damage;
        enemy.target.timeToBurn = timeBeforeDamage;
    }
}
