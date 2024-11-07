
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public MainMenuSettings settings { get; private set; }

    [SerializeField] UIMainMenuScript uiMenuScript;
    [SerializeField] UIStartGameScript startGameScript;

    public void Init(MainMenuSettings _settings)
    {
        settings = _settings;

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

    public void UpdatePlayerCoinsText(int count) { uiMenuScript.UpdateText(count); }

    private void HideAllScreens()
    {
        uiMenuScript.CloseScreen();
        startGameScript.CloseScreen();
    }
}
