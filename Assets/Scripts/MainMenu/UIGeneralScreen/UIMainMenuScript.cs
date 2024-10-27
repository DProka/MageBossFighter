
using UnityEngine;

public class UIMainMenuScript : MonoBehaviour, IMenuScreen
{
    [SerializeField] Transform upperPart;
    [SerializeField] Transform lowerPart;

    private Canvas mainCanvas;
    private UIMenuController uiController;

    public void Init(UIMenuController _uiController)
    {
        mainCanvas = GetComponent<Canvas>();
        uiController = _uiController;
    }

    public void OpenStartGameMenu() { uiController.OpenScreen(UIMenuController.Screen.StartGame); }

    public void OpenScreen()
    {
        MainMenuController.Instance.LoadMenuPreviev();

        SwitchActive(true);
    }

    public void CloseScreen()
    {
        SwitchActive(false);
    }

    private void SwitchActive(bool isActive) { mainCanvas.enabled = isActive; }
}
