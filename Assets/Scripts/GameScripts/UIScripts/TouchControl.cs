
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public PlayerScript player;

    public bool canShoot = false;
    public float timeBeforeShooting = 0.5f;
    public float timerBefore;

    //public void UpdateTouch()
    //{
    //    //if (player.shootingScript.shootTimer <= 0)
    //    //{
    //    //    if (Input.GetMouseButtonDown(0))
    //    //    {
    //    //        canShoot = true;
    //    //    }
    //    //}

    //    //if (canShoot)
    //    //{
    //    //    timerBefore += Time.deltaTime;
    //    //}
    //    //else
    //    //    timerBefore = 0;

    //    //if (timerBefore >= timeBeforeShooting)
    //    //{
    //    //    player.shootingScript.Shoot();
    //    //    timerBefore = 0;
    //    //    canShoot = false;
    //    //}
    //}

    public void OnBeginDrag(PointerEventData eventData)
    {
        //canShoot = false;

        //if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)) && GameController.Instance.gameIsActive)
        //{
            
        //    player.movementScript.CheckTargetPoint();

        //    if(!player.movementScript.isMoving)
        //    {
        //        if (eventData.delta.x < 0)
        //        {
        //            player.movementScript.nextPoint++;
        //            if (player.movementScript.nextPoint >= GameController.Instance.points.Length)
        //                player.movementScript.nextPoint = 0;

        //            player.movementScript.ChangePoint(player.movementScript.nextPoint);
                    
        //        }

        //        else
        //        {
        //            player.movementScript.nextPoint--;

        //            if (player.movementScript.nextPoint < 0)
        //                player.movementScript.nextPoint = GameController.Instance.points.Length - 1;

        //            player.movementScript.ChangePoint(player.movementScript.nextPoint);
                    
        //        }
        //    }
        //}
    }

    public void OnDrag(PointerEventData eventData) { }

}
