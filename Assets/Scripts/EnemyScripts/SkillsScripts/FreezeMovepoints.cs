
using UnityEngine;

public class FreezeMovepoints : MonoBehaviour
{
    public EnemyController enemy;
    public float damage;
    public float timeBeforeDamage;
    public float powerOfFreeze;
    public float timeFreezeStatus;
    public float cooldown;

    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = cooldown;
        SetDamage();
        enemy.UpdEnemy.AddListener(SkillUpdate);
    }

    void SkillUpdate()
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
        if (status != Status.Freeze)
        {
            GameController.gameController.points[random].GetNewStatus(Status.Freeze);
            return;
        }
        else
        {
            for (int i = 0; i < GameController.gameController.points.Length; i++)
            {
                status = GameController.gameController.points[i].pointStatus;
                if (status != Status.Freeze)
                {
                    GameController.gameController.points[i].GetNewStatus(Status.Freeze);
                    return;
                }
            }
        }
    }

    void SetDamage()
    {
        enemy.freezeDamage = damage;
        enemy.player.timeBeforeFreezeDamage = timeBeforeDamage;
        enemy.freezeStatusTimer = timeFreezeStatus;
        enemy.powerOfFreeze = powerOfFreeze;
    }
}
