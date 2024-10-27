using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Main")]

    [SerializeField] UIArenaTimer arenaTimer;

    [Header("Player UI")]

    [SerializeField] HealthBar playerHB;
    [SerializeField] UIPlayerControls playerControls;

    [Header("Boss")]

    [SerializeField] HealthBar bossHB;

    [Header("End Round")]

    [SerializeField] UIEndRoundScreen endRoundScreen;

    [Header("FPS Meter")]

    [SerializeField] UIFpsMeter fpsMeter;

    private GameController gameController;
    private Canvas mainCanvas;

    public void Init(GameController _gameController)
    {
        Instance = this;
        gameController = _gameController;

        mainCanvas = GetComponent<Canvas>();

        playerControls.Init(this);
        endRoundScreen.Init();
    }

    public void UpdateUI()
    {
        fpsMeter.ShowFPS();
    }

    #region Game

    public void UpdateArenaTimer(float time) => arenaTimer.UpdateTimer(time);

    public void StartArena()
    {
        arenaTimer.SwitchTimerActive(false);
        endRoundScreen.SwitchMainCanvas(false);
    }

    public void RestartScene()
    {
        arenaTimer.SwitchTimerActive(true);
        gameController.RestartScene();
    }

    public void GoToMaiuMenu() => gameController.GoToMaiuMenu();

    #endregion

    #region HealthBars

    public void UpdateHealthBar(bool isPlayer, float maxHealth, float currentHealth)
    {
        if (isPlayer)
            playerHB.SetHealth(maxHealth, currentHealth);
        else
            bossHB.SetHealth(maxHealth, currentHealth);
    }

    #endregion

    #region Screens

    public void CallEndScreen(bool win) => endRoundScreen.CallScreen(win);

    #endregion
}
