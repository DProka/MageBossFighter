
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] UIMainMenuScript uiMenuScript;
    [SerializeField] UIStartGameScript startGameScript;

    public void Init()
    {
        uiMenuScript.Init(this);
        startGameScript.Init(this);

        HideAllScreens();
        OpenScreen(Screen.MainMenu);
    }

    public void OpenScreen(Screen screen)
    {
        HideAllScreens();

        switch (screen)
        {
            case Screen.MainMenu:
                uiMenuScript.OpenScreen();
                break;

            case Screen.StartGame:
                startGameScript.OpenScreen();
                break;
        }
    }

    public enum Screen
    {
        MainMenu,
        StartGame,
    }

    private void HideAllScreens()
    {
        uiMenuScript.CloseScreen();
        startGameScript.CloseScreen();
    }
}
