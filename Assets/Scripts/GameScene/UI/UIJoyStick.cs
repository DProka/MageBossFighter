
using UnityEngine;

public class UIJoyStick : MonoBehaviour
{
    [SerializeField] float stickRadius = 50f;
    [SerializeField] float activationDistance = 0.2f;
    [SerializeField] Vector2[] activeArea;

    private RectTransform stickTransform;

    private Vector2 startBackPosition;
    private Vector2 startStickPosition;

    private bool isDragging;
    // 582 334
    private void Start()
    {
        stickTransform = transform.GetChild(0).GetComponent<RectTransform>();

        startBackPosition = transform.position;
        startStickPosition = stickTransform.position;

        isDragging = false;

        Debug.Log("Screen width " + Screen.width);
        Debug.Log("Joystick positiona " + startBackPosition);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            Vector2 mousePosition = Input.mousePosition;

            Debug.Log("Mouse Position " + mousePosition);

            //if (mousePosition.x < Screen.width / 2)
            //if (mousePosition.x < activeArea[0].x && mousePosition.x < activeArea[1].x && mousePosition.y > activeArea[0].y && mousePosition.y < activeArea[1].y)
            if (mousePosition.x > activeArea[0].x && mousePosition.x < activeArea[1].x && mousePosition.y > activeArea[0].y && mousePosition.y < activeArea[1].y)
            {
                transform.position = Input.mousePosition;
                isDragging = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            ResetPosition();
        }

        if (isDragging)
            DragStick();
    }

    private void DragStick()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 direction = currentMousePosition - (Vector2)transform.position;
        float distance = Mathf.Clamp(direction.magnitude, 0, stickRadius);
        Vector2 clampedPosition = (Vector2)transform.position + direction.normalized * distance;
        stickTransform.position = clampedPosition;

        Debug.Log("direction " + direction);

        GameController.Instance.player.MovePlayer(direction.x);
    }

    public Vector2 GetNormalizedDirection()
    {
        if (!isDragging) return Vector2.zero;

        Vector2 direction = stickTransform.position - transform.position;
        return direction.normalized;
    }

    private void ResetPosition()
    {
        transform.position = startBackPosition;
        stickTransform.position = startStickPosition;
    }
}
