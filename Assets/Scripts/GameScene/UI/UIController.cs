using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Main")]

    [SerializeField] UIArenaTimer arenaTimer;

    [Header("Player UI")]

    [SerializeField] UIPlayerInfo playerInfoPart;
    [SerializeField] UIPlayerControls playerControls;
    [SerializeField] GameObject tutorialPart;

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

    private bool controlsIsActive;

    public void Init(GameController _gameController)
    {
        Instance = this;
        gameController = _gameController;

        mainCanvas = GetComponent<Canvas>();

        playerControls.Init(this);
        playerInfoPart.Init(this);
        endRoundScreen.Init();
        pauseScreen.Init();
    }

    public void UpdateUI()
    {
        fpsMeter.ShowFPS();
        playerControls.UpdateScript();
    }

    #region Game

    public void UpdateArenaTimer(float time) => arenaTimer.UpdateTimer(time);

    public void SwitchPauseGame(bool isPaused)
    {
        pauseScreen.SwitchPauseGame(isPaused);
        SwitchControlsActive(!isPaused);
    }

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

    public void SwitchControlsActive(bool isActive)
    {
        controlsIsActive = isActive;
        playerControls.SwitchControlsActive(isActive);

        Debug.Log("Controls Is Active = " + isActive);
    }

    #endregion

    #region HealthBars

    public void UpdateHealthBar(bool isPlayer, float maxHealth, float currentHealth)
    {
        if (isPlayer)
            playerInfoPart.UpdateHealthBar(maxHealth, currentHealth);
        else
            bossHB.SetFillAmount(maxHealth, currentHealth);
    }

    public void SetBossName(string name) => bossNameText.text = name;

    #endregion

    #region Player Info

    public void UpdateStatus(PlayerStatus.Status type, float time) => playerInfoPart.UpdateStatus(type, time);

    public void SetStatusVisibility(PlayerStatus.Status type, bool isVisible) => playerInfoPart.SetStatusVisibility(type, isVisible);

    public void UpdateComboBar(float maxHealth, float currentHealth) => playerInfoPart.UpdateComboBar(maxHealth, currentHealth);

    #endregion

    #region Screens

    public void StartGame()
    {
        arenaTimer.SwitchTimerActive(true);
        gameController.StartArenaTimer();
    }

    public void SwitchTutorialPart(bool isActive)
    {
        tutorialPart.SetActive(isActive);
        SwitchControlsActive(!isActive);
    }

    public void CallEndScreen(bool win) => endRoundScreen.CallScreen(win);
    
    public void CloseEndScreen() => endRoundScreen.CloseScreen();

    #endregion
}
