using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Main")]

    public TextMeshProUGUI levelNumber;
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI timerText;
    public GameObject timerObj;

    [Header("Player")]

    [SerializeField] HealthBar playerHB;

    [Header("Boss")]

    [SerializeField] HealthBar bossHB;

    public TextMeshProUGUI bossName;

    [Header("End of Lvl")]

    public GameObject endMenu;
    public TextMeshProUGUI endText;

    [Header("FPS Meter")]
    public float fps;
    public TextMeshProUGUI fpsText;
    public float timer;

    private GameController gameController;

    public void Init(GameController _gameController)
    {
        Instance = this;
        gameController = _gameController;
    }

    public void UpdateUI()
    {
        ShowFPS();
    }

    public void ShowFPS()
    {
        fps = 1.0f / Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= 0.3f)
        {
            fpsText.text = "FPS: " + (int)fps;
            timer = 0f;
        }
    }

    public void CallEndScreen(bool win)
    {
        endMenu.SetActive(true);

        endText.text = win ? "YOU WIN" : "YOU LOSE";
    }

    public void UpdateHealthBar(bool isPlayer, float maxHealth, float currentHealth)
    {
        if (isPlayer)
            playerHB.SetHealth(maxHealth, currentHealth);
        else
            bossHB.SetHealth(maxHealth, currentHealth);
    }

    public void RestartScene() => gameController.RestartScene();
    
    public void GoToMaiuMenu() => gameController.GoToMaiuMenu();
}
