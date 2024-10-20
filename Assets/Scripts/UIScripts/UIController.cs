using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Main")]

    public TextMeshProUGUI levelNumber;
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI timerText;
    public GameObject timerObj;

    [Header("Player")]

    public HealthBar playerHB;

    [Header("Boss")]

    public HealthBar bossHB;
    public TextMeshProUGUI bossName;

    [Header("End of Lvl")]

    public GameObject endMenu;
    public TextMeshProUGUI endText;

    [Header("FPS Meter")]
    public float fps;
    public TextMeshProUGUI fpsText;
    public float timer;

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

    public void UpdateNames(string _bossName, string _levelName) 
    {
        bossName.text = _bossName;
        levelName.text = _levelName;
    }

    public void CallEndScreen(bool win)
    {
        endMenu.SetActive(true);

        if (win)
        {
            endText.text = "YOU WIN";
        }
        else
        {
            endText.text = "YOU LOSE";
        }
    }

    public void RestartScene()
    {
        GameController.gameController.RestartScene();
    }

    public void GoToMaiuMenu()
    {
        SceneManager.LoadScene(0);
    }
}
