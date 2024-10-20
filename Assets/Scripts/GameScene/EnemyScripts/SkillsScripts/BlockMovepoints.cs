
using UnityEngine;

public class BlockMovepoints : MonoBehaviour
{
    public EnemyController enemy;
    public float cooldown;
    public PlayerController player;
    
    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = cooldown;
        enemy.UpdEnemy.AddListener(SkillUpdate);
    }

    public void SkillUpdate()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        else
        {
            ChoosePoint(player.movementScript.nextPoint);
            cooldownTimer = cooldown;
        }
    }

    void ChoosePoint(int playerPoint)
    {
        int point1 = CheckPoint(playerPoint + 1);
        int point2 = CheckPoint(playerPoint - 1);

        int random = Random.Range(0, GameController.Instance.points.Length);
        Status status = GameController.Instance.points[random].pointStatus;

        if (random != playerPoint && random != point1 && random != point2 && status != Status.Blocked)
        {
            GameController.Instance.points[random].GetNewStatus(Status.Blocked);
            return;

        }

        else
        {
            for (int i = 0; i < GameController.Instance.points.Length; i++)
            {
                status = GameController.Instance.points[i].pointStatus;
                if (i != playerPoint && i != point1 && i != point2 && status != Status.Blocked)
                {
                    GameController.Instance.points[i].GetNewStatus(Status.Blocked);
                    return;
                }
            }
        }
    }

    int CheckPoint(int point)
    {
        int point1 = point;
        if (point1 >= GameController.Instance.points.Length)
            point1 = 0;
        else if (point1 < 0)
            point1 = 11;

        return point1;
    }
}
