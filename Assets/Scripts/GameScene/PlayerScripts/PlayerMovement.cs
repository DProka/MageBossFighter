
using UnityEngine;

public class PlayerMovement
{
    //public MovePointPrefabScript targetPoint { get; private set; }

    public bool isMoving { get; private set; }
    public int lastPoint { get; private set; }

    private PlayerScript player;
    private PlayerSettings settings;
    private Transform enemyPoint;

    private float mouseDeltaX;
    private float moveTimer;

    private int nextPoint = 0;

    public PlayerMovement(PlayerScript playerController, PlayerSettings _settings, Transform _enemyPoint)
    {
        player = playerController;
        settings = _settings;
        enemyPoint = _enemyPoint;

        //targetPoint = ArenaManager.Instance.GetMovePointByNum(nextPoint);

        lastPoint = 0;
        moveTimer = 0;
    }

    public void UpdateScript()
    {
        MovePlayer();

        UpdateKeyboard();
    }

    public void ResetWayPoint()
    {
        nextPoint = 0;
        //targetPoint = ArenaManager.Instance.GetMovePointByNum(nextPoint);
        //player.transform.position = targetPoint.transform.position;
        player.transform.position = ArenaManager.Instance.GetMovePointPositionByNum(nextPoint);
    }

    public void UIChangePointByButton(bool isLeft)
    {
        if (moveTimer > 0)
            return;

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

    private void MovePlayer()
    {
        //if (targetPoint == null)
        //    return;

        //player.transform.position = Vector3.MoveTowards(player.transform.position, targetPoint.transform.position, settings.moveSpeed * Time.deltaTime);
        player.transform.position = Vector3.MoveTowards(player.transform.position, ArenaManager.Instance.GetMovePointPositionByNum(nextPoint), settings.moveSpeed * Time.deltaTime);
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
        //float distance = Vector3.Distance(player.transform.position, targetPoint.transform.position);
        float distance = Vector3.Distance(player.transform.position, ArenaManager.Instance.GetMovePointPositionByNum(nextPoint));

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
        //MovePointPrefabScript newPoint = ArenaManager.Instance.GetMovePointByNum(num);
        //if (newPoint.currentStatus != ArenaManager.PointStatus.Blocked)
        //{
        //    targetPoint = newPoint;
        //    player.CheckWaypointStatus();
        //    moveTimer = settings.moveDelay;

        //    lastPoint = nextPoint;

        //    //Debug.Log("Target Point = " + targetPoint.name);
        //}
        //else
        //{
        //    nextPoint = lastPoint;
        //}
        
        if (!ArenaManager.Instance.CheckPointIsBlockedByNum(num))
        {
            player.CheckWaypointStatus();
            moveTimer = settings.moveDelay;

            lastPoint = nextPoint;
        }
        else
        {
            nextPoint = lastPoint;
        }
    }
}
