﻿
using UnityEngine;

public class PlayerMovement
{
    public bool isMoving { get; private set; }
    public MovePointPrefabScript targetPoint { get; private set; }

    private PlayerScript player;
    private PlayerSettings settings;
    private Transform enemyPoint;
    //private Transform nextWayPoint;

    private float mouseDeltaX;
    private float moveTimer;

    private int nextPoint = 0;
    private int lastPoint = 0;

    public PlayerMovement(PlayerScript playerController, PlayerSettings _settings, Transform _enemyPoint)
    {
        player = playerController;
        settings = _settings;
        enemyPoint = _enemyPoint;

        targetPoint = GameController.Instance.points[nextPoint];

        moveTimer = 0;
    }

    public void UpdateScript()
    {
        MovePlayer();

        //UpdateMouse();
        UpdateKeyboard();
    }

    public void ResetWayPoint()
    {
        SetNewPointByNum(0);
    }

    private void MovePlayer()
    {
        //if (nextWayPoint == null)
        if (targetPoint == null)
            return;

        //player.transform.position = Vector3.MoveTowards(player.transform.position, nextWayPoint.position, settings.moveSpeed * Time.deltaTime);
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPoint.transform.position, settings.moveSpeed * Time.deltaTime);
        RotatePlayer();
        CheckIsMoving();
    }

    private void RotatePlayer()
    {
        Vector3 direction = (enemyPoint.position - player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, lookRotation, Time.deltaTime * 100);
    }

    private void CheckIsMoving()
    {
        //float distance = Vector3.Distance(player.transform.position, nextWayPoint.position);
        float distance = Vector3.Distance(player.transform.position, targetPoint.transform.position);

        if (distance > 0.1f)
            isMoving = true;
        else
        {
            if (moveTimer > 0)
                moveTimer -= Time.deltaTime;
            else
                isMoving = false;

            player.playerAnimator.StartAnimation(PlayerAnimator.Clip.Idle);
        }

        //Debug.Log("player is moving = " + isMoving);
    }

    //private void UpdateMouse()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        mouseDeltaX = Input.GetAxis("Mouse X");
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        //ChangePoint();
    //    }
    //}

    //private void ChangePoint()
    //{
    //    if (mouseDeltaX < -0.3)
    //    {
    //        player.playerAnimator.StartAnimation(PlayerAnimator.Clip.MoveLeft);
    //        nextPoint++;
    //    }
    //    else if (mouseDeltaX > 0.3)
    //    {
    //        player.playerAnimator.StartAnimation(PlayerAnimator.Clip.MoveRight);
    //        nextPoint--;
    //    }

    //    if (nextPoint >= 12)
    //        nextPoint = 0;
    //    if (nextPoint < 0)
    //        nextPoint = 11;

    //    targetPoint = GameController.Instance.points[nextPoint];
    //    //nextWayPoint = targetPoint.transform;
    //    //nextWayPoint = GameController.Instance.GetNextWayPoint(nextPoint);
    //    player.CheckWaypointStatus();
    //    moveTimer = settings.moveDelay;

    //    Debug.Log("Target Point = " + targetPoint.name);
    //}

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
            player.playerAnimator.StartAnimation(PlayerAnimator.Clip.MoveLeft);
            nextPoint++;
        }
        else
        {
            player.playerAnimator.StartAnimation(PlayerAnimator.Clip.MoveRight);
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
        //nextWayPoint = GameController.Instance.GetNextWayPoint(num);

        MovePointPrefabScript newPoint = GameController.Instance.points[num];
        if (newPoint.pointStatus != MovePointPrefabScript.Status.Blocked)
        {
            targetPoint = newPoint;
            player.CheckWaypointStatus();
            moveTimer = settings.moveDelay;

            lastPoint = nextPoint;

            Debug.Log("Target Point = " + targetPoint.name);
        }
        else
        {
            nextPoint = lastPoint;
        }
    }
}
