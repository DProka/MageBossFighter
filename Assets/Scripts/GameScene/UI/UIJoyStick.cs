
using UnityEngine;

public class UIJoyStick : MonoBehaviour
{
    [SerializeField] private float stickRadius = 50f; // Радиус перемещения стика
    private RectTransform stickTransform; // Ссылка на RectTransform стика
    private Vector2 initialBackPosition; // Начальная позиция фона джойстика
    private Vector2 initialStickPosition; // Начальная позиция стика
    private bool isDragging = false; // Флаг, указывающий, происходит ли перетаскивание
    private int activeTouchId = -1; // Идентификатор активного касания

    private void Start()
    {
        // Получаем RectTransform стика (первый дочерний элемент)
        stickTransform = transform.GetChild(0).GetComponent<RectTransform>();

        // Сохраняем начальные позиции
        initialBackPosition = transform.position;
        initialStickPosition = stickTransform.position;
    }

    public void UpdateScript()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        // Проверяем, есть ли активные касания
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Начало касания
                if (touch.phase == TouchPhase.Began && !isDragging)
                {
                    if (touch.position.x < Screen.width / 2) // Ограничиваем область активации джойстика
                    {
                        transform.position = touch.position; // Перемещаем фон джойстика
                        isDragging = true;
                        activeTouchId = touch.fingerId; // Запоминаем ID текущего касания
                    }
                }

                // Перетаскивание
                if (isDragging && touch.fingerId == activeTouchId && touch.phase == TouchPhase.Moved)
                {
                    DragStick(touch.position);
                }

                // Завершение касания
                if (isDragging && touch.fingerId == activeTouchId && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                {
                    isDragging = false;
                    activeTouchId = -1; // Сбрасываем ID активного касания
                    ResetPosition();
                }
            }
        }
    }

    private void DragStick(Vector2 touchPosition)
    {
        Vector2 direction = touchPosition - (Vector2)transform.position; // Направление от центра джойстика к точке касания
        float distance = Mathf.Clamp(direction.magnitude, 0, stickRadius); // Ограничиваем расстояние до радиуса стика
        Vector2 clampedPosition = (Vector2)transform.position + direction.normalized * distance; // Вычисляем позицию с учетом радиуса
        stickTransform.position = clampedPosition; // Перемещаем стик

        GameController.Instance.player.MovePlayer(direction.x);
    }

    private void ResetPosition()
    {
        // Возвращаем фон и стик в начальные позиции
        transform.position = initialBackPosition;
        stickTransform.position = initialStickPosition;
    }

    //[SerializeField] float stickRadius = 50f;
    //[SerializeField] float activationDistance = 0.2f;
    //[SerializeField] Vector2[] activeArea;

    //private RectTransform stickTransform;

    //private Vector2 startBackPosition;
    //private Vector2 startStickPosition;

    //private bool isDragging;
    //// 582 334
    //private void Start()
    //{
    //    stickTransform = transform.GetChild(0).GetComponent<RectTransform>();

    //    startBackPosition = transform.position;
    //    startStickPosition = stickTransform.position;

    //    isDragging = false;

    //    Debug.Log("Screen width " + Screen.width);
    //    Debug.Log("Joystick positiona " + startBackPosition);
    //}

    //private void LateUpdate()
    //{
    //    if (Input.GetMouseButtonDown(0) && !isDragging)
    //    {
    //        Vector2 mousePosition = Input.mousePosition;

    //        Debug.Log("Mouse Position " + mousePosition);

    //        //if (mousePosition.x < Screen.width / 2)
    //        //if (mousePosition.x < activeArea[0].x && mousePosition.x < activeArea[1].x && mousePosition.y > activeArea[0].y && mousePosition.y < activeArea[1].y)
    //        if (mousePosition.x > activeArea[0].x && mousePosition.x < activeArea[1].x && mousePosition.y > activeArea[0].y && mousePosition.y < activeArea[1].y)
    //        {
    //            transform.position = Input.mousePosition;
    //            isDragging = true;
    //        }
    //    }
    //    else if (Input.GetMouseButtonUp(0))
    //    {
    //        isDragging = false;
    //        ResetPosition();
    //    }

    //    if (isDragging)
    //        DragStick();
    //}

    //private void DragStick()
    //{
    //    Vector2 currentMousePosition = Input.mousePosition;
    //    Vector2 direction = currentMousePosition - (Vector2)transform.position;
    //    float distance = Mathf.Clamp(direction.magnitude, 0, stickRadius);
    //    Vector2 clampedPosition = (Vector2)transform.position + direction.normalized * distance;
    //    stickTransform.position = clampedPosition;

    //    Debug.Log("direction " + direction);

    //    GameController.Instance.player.MovePlayer(direction.x);
    //}

    //public Vector2 GetNormalizedDirection()
    //{
    //    if (!isDragging) return Vector2.zero;

    //    Vector2 direction = stickTransform.position - transform.position;
    //    return direction.normalized;
    //}

    //private void ResetPosition()
    //{
    //    transform.position = startBackPosition;
    //    stickTransform.position = startStickPosition;
    //}
}
