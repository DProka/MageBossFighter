
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public MainMenuSettings settings { get; private set; }

    [SerializeField] UIMainMenuScript uiMenuScript;
    [SerializeField] UIStartGameScript startGameScript;
    [SerializeField] UIStatsMenu statsMenu;

    public void Init(MainMenuSettings _settings)
    {
        settings = _settings;

        uiMenuScript.Init(this);
        startGameScript.Init(this);
        statsMenu.Init(this);

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
        
            case Screen.StatsMenu:
                statsMenu.OpenMenu();
                break;
        }
    }

    public enum Screen
    {
        MainMenu,
        StartGame,
        StatsMenu,
    }

    public void UpdatePlayerCoinsText(int count) { uiMenuScript.UpdateText(count); }

    private void HideAllScreens()
    {
        uiMenuScript.CloseScreen();
        startGameScript.CloseScreen();
        statsMenu.CloseMenu();
    }
}
