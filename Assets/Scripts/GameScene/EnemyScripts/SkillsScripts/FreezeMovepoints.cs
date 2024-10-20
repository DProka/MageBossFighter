
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
        int random = Random.Range(0, GameController.Instance.points.Length);
        Status status = GameController.Instance.points[random].pointStatus;
        if (status != Status.Freeze)
        {
            GameController.Instance.points[random].GetNewStatus(Status.Freeze);
            return;
        }
        else
        {
            for (int i = 0; i < GameController.Instance.points.Length; i++)
            {
                status = GameController.Instance.points[i].pointStatus;
                if (status != Status.Freeze)
                {
                    GameController.Instance.points[i].GetNewStatus(Status.Freeze);
                    return;
                }
            }
        }
    }

    void SetDamage()
    {
        enemy.freezeDamage = damage;
        enemy.target.timeBeforeFreezeDamage = timeBeforeDamage;
        enemy.freezeStatusTimer = timeFreezeStatus;
        enemy.powerOfFreeze = powerOfFreeze;
    }
}
