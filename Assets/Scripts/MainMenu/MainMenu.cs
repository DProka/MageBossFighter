using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour 
{
    public int levelCount;

    [Header("Main Buttons")]

    public Button startButton;

    [Header("Bosses")]

    public Transform bossPoint;
    public TextMeshProUGUI bossName;
    public GameObject[] bossesArray;

    private int bossModelID = 0;
    private GameObject activeBoss;

    [Header("Levels")]

    public TextMeshProUGUI levelText;
    public GameObject[] levelsArray;

    void Awake()
    {
        levelCount = DataHolder.gameLevel;
        startButton.onClick.AddListener(StartNewGame);
        UpdateBossModel(levelCount);
    }

    void StartNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ChangeLevel(bool right)
    {
        if (right)
        {
            bossModelID += 1;
            if (bossModelID > bossesArray.Length - 1)
            {
                bossModelID = 0;
            }
        }
        else
        {
            bossModelID -= 1;
            if (bossModelID < 0)
            {
                bossModelID = bossesArray.Length - 1;
            }
        }

        levelCount = bossModelID;
        levelText.text = "LEVEL " + (levelCount + 1);
        UpdateBossModel(bossModelID);
        SaveData();
    }

    public void UpdateBossModel(int modelID)
    {
        if (activeBoss != null)
        {
            Destroy(activeBoss);
        }
        activeBoss = Instantiate(bossesArray[modelID], bossPoint.position, Quaternion.identity);
        bossName.text = bossesArray[modelID].name;
    }

    private void SaveData()
    {
        DataHolder.gameLevel = levelCount;
    }
}
