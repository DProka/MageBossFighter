
using UnityEngine;

public class PlayerMovement
{
    public bool isMoving { get; private set; }

    private MovePoint targetPoint;

    private PlayerScript controller;
    private PlayerSettings settings;
    private Transform enemyPoint;
    private Transform nextWayPoint;

    private float mouseDeltaX;
    private float moveTimer;

    private int nextPoint = 0;
    private int lastPoint;

    public PlayerMovement(PlayerScript playerController, PlayerSettings _settings, Transform _enemyPoint)
    {
        controller = playerController;
        settings = _settings;
        enemyPoint = _enemyPoint;

        targetPoint = GameController.Instance.points[nextPoint];

        moveTimer = 0;
    }

    public void UpdateMovement()
    {
        MovePlayer();

        //UpdateMouse();
        UpdateKeyboard();
    }

    public void ResetWayPoint()
    {
        SetNewPointByNum(0);
    }

    //public void CheckTargetPoint()
    //{
    //    if (targetPoint.pointStatus == Status.Burning)
    //        controller.burnStatus = PlayerScript.PlayerStatus.Burn;
    //    else
    //        controller.burnStatus = PlayerScript.PlayerStatus.NoStatus;

    //    if (targetPoint.pointStatus == Status.Freeze)
    //        controller.freezeStatus = PlayerScript.PlayerStatus.Freeze;
    //    else
    //        controller.freezeStatus = PlayerScript.PlayerStatus.NoStatus;

    //    if (targetPoint.pointStatus != Status.Blocked)
    //        lastPoint = nextPoint;
    //    else
    //        nextPoint = lastPoint;
    //}

    private void ChangePoint()
    {
        if (mouseDeltaX < -0.3)
        {
            controller.playerAnimator.StartAnimation(PlayerAnimator.Clip.MoveLeft);
            nextPoint++;
        }
        else if (mouseDeltaX > 0.3)
        {
            controller.playerAnimator.StartAnimation(PlayerAnimator.Clip.MoveRight);
            nextPoint--;
        }

        if (nextPoint >= 12)
            nextPoint = 0;
        if (nextPoint < 0)
            nextPoint = 11;

        nextWayPoint = GameController.Instance.GetNextWayPoint(nextPoint);

        targetPoint = GameController.Instance.points[nextPoint];
        Debug.Log("Target Point = " + targetPoint.name);

        moveTimer = settings.moveDelay;
    }

    private void MovePlayer()
    {
        if (nextWayPoint == null)
            return;

        controller.transform.position = Vector3.MoveTowards(controller.transform.position, nextWayPoint.position, settings.moveSpeed * Time.deltaTime);
        RotatePlayer();
        CheckIsMoving();
    }

    private void RotatePlayer()
    {
        Vector3 direction = (enemyPoint.position - controller.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, lookRotation, Time.deltaTime * 100);
    }

    private void CheckIsMoving()
    {
        float distance = Vector3.Distance(controller.transform.position, nextWayPoint.position);

        if (distance > 0.1f)
            isMoving = true;
        else
        {
            if (moveTimer > 0)
                moveTimer -= Time.deltaTime;
            else
                isMoving = false;

            controller.playerAnimator.StartAnimation(PlayerAnimator.Clip.Idle);
        }

        //Debug.Log("player is moving = " + isMoving);
    }

    private void UpdateMouse()
    {
        if (Input.GetMouseButton(0))
        {
            mouseDeltaX = Input.GetAxis("Mouse X");
        }

        if (Input.GetMouseButtonUp(0))
        {
            ChangePoint();
        }
    }

    private void UpdateKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangePointByButton(true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangePointByButton(false);
        }
    }

    private void ChangePointByButton(bool isLeft)
    {
        if (isLeft)
        {
            controller.playerAnimator.StartAnimation(PlayerAnimator.Clip.MoveLeft);
            nextPoint++;
        }
        else
        {
            controller.playerAnimator.StartAnimation(PlayerAnimator.Clip.MoveRight);
            nextPoint--;
        }

        if (nextPoint >= 12)
            nextPoint = 0;
        if (nextPoint < 0)
            nextPoint = 11;

        SetNewPointByNum(nextPoint);
    }

    private void SetNewPointByNum(int num)
    {
        nextWayPoint = GameController.Instance.GetNextWayPoint(num);

        targetPoint = GameController.Instance.points[nextPoint];
        Debug.Log("Target Point = " + targetPoint.name);

        moveTimer = settings.moveDelay;
    }
}
