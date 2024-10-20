
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float moveDelay;

    [HideInInspector] public MovePoint targetPoint;
    [HideInInspector] public float moveTimer;
    [HideInInspector] public int nextPoint = 0;
    [HideInInspector] public bool isMoving;
    private int lastPoint;
    private PlayerController playerController;


    public void Init()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        targetPoint = GameController.gameController.points[nextPoint];
    }

    public void UpdateMovement()
    {
        MovePlayer();
     
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
            isMoving = true;
        }
        else
        {
            isMoving = false;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                nextPoint++;
                if (nextPoint >= GameController.gameController.points.Length)
                    nextPoint = 0;
                ChangePoint(nextPoint);
                StartCoroutine(playerController.PlayStepAnimation(true));
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextPoint--;
                if (nextPoint < 0)
                    nextPoint = GameController.gameController.points.Length - 1;
                ChangePoint(nextPoint);
                StartCoroutine(playerController.PlayStepAnimation(false));
            }
        }

        playerController.isMoving = isMoving;
    }

    public void CheckTargetPoint()
    {
        if (targetPoint.pointStatus == Status.Burning)
            playerController.burnStatus = PlayerController.PlayerStatus.Burn;
        else
            playerController.burnStatus = PlayerController.PlayerStatus.NoStatus;

        if (targetPoint.pointStatus == Status.Freeze)
            playerController.freezeStatus = PlayerController.PlayerStatus.Freeze;
        else
            playerController.freezeStatus = PlayerController.PlayerStatus.NoStatus;

        if (targetPoint.pointStatus != Status.Blocked)
            lastPoint = nextPoint;
        else
            nextPoint = lastPoint;
    }

    public void ChangePoint(int newPoint)
    {
        targetPoint = GameController.gameController.points[newPoint];
        CheckTargetPoint();
        moveTimer = moveDelay;
    }

    void MovePlayer()
    {
        transform.position = Vector3.Lerp(GameController.gameController.points[nextPoint].transform.position, transform.position, moveSpeed);
        RotatePlayer();
    }

    void RotatePlayer()
    {
        Vector3 direction = (playerController.enemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 100);
    }
}
