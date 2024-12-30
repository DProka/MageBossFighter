
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPlayerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected bool isPressed = false;

    [SerializeField] Button button;

    public void UpdateScript()
    {
        if (isPressed)
        {
            ButtonWorks();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public virtual void ButtonWorks() { }
}
