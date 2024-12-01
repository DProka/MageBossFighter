
using UnityEngine;
using TMPro;

public class UIMainMenuScript : MonoBehaviour, IMenuScreen
{
    [SerializeField] Transform upperPart;
    [SerializeField] Transform lowerPart;

    [SerializeField] TextMeshProUGUI coinsText;

    private Canvas mainCanvas;
    private UIMenuController uiController;

    public void Init(UIMenuController _uiController)
    {
        mainCanvas = GetComponent<Canvas>();
        uiController = _uiController;
    }

    public void OpenStartGameMenu() { uiController.OpenScreen(UIMenuController.Screen.StartGame); }

    public void UpdateText(int count) => coinsText.text = "" + count;

    #region Main Screen

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

    #endregion
}
