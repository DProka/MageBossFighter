
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuTouch : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public MainMenu mainMenu;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if (eventData.delta.x > 0)
            {
                mainMenu.ChangeLevel(false);
            }

            else
            {
                mainMenu.ChangeLevel(true);
            }
        }
    }

    public void OnDrag(PointerEventData eventData) { }
}
