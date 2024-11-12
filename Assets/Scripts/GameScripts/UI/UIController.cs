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
    [SerializeField] UIPlayerStatusManager playerStatusManager;

    [Header("Boss")]

    [SerializeField] HealthBar bossHB;
    [SerializeField] TextMeshProUGUI bossNameText;

    [Header("Pause Game")]

    [SerializeField] UIPauseGameScreen pauseScreen;

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
        playerStatusManager.Init();

        endRoundScreen.Init();
        pauseScreen.Init();
    }

    public void UpdateUI()
    {
        fpsMeter.ShowFPS();
    }

    #region Game

    public void UpdateArenaTimer(float time) => arenaTimer.UpdateTimer(time);

    public void SwitchPauseGame(bool isPaused) => pauseScreen.SwitchPauseGame(isPaused);

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

    public void SetBossName(string name) => bossNameText.text = name;

    #endregion

    #region Statuses

    public void UpdateStatus(PlayerStatus.Status type, float time) => playerStatusManager.UpdateStatus(type, time);

    public void SetStatusVisibility(PlayerStatus.Status type, bool isVisible) => playerStatusManager.SetStatusVisibility(type, isVisible);

    #endregion

    #region Screens

    public void CallEndScreen(bool win) => endRoundScreen.CallScreen(win);
    
    public void CloseEndScreen() => endRoundScreen.CloseScreen();

    #endregion
}
