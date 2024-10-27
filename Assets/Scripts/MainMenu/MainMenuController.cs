
using UnityEngine;

public class MainMenuController : MonoBehaviour 
{
    public static MainMenuController Instance;

    [SerializeField] MainMenuSettings settings;

    [Header("Preview Manager")]

    [SerializeField] MainMenuPreviewManager previewManager;

    [Header("UI Script")]

    [SerializeField] UIMenuController uiController;

    private int levelCount;

    void Start()
    {
        Instance = this;

        levelCount = 0;

        previewManager.Init(settings);

        uiController.Init();

        InitializePreview();
    }

    public void StartNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadMenuPreviev()
    {
        previewManager.SpawnArenaByNum(levelCount);
        previewManager.SpawnPlayerByNum(levelCount);
    }
    
    public void LoadLevelPreviev(int lvlNum)
    {
        previewManager.SpawnArenaByNum(lvlNum);
        previewManager.SpawnBossByNum(lvlNum);
    }

    private void InitializePreview()
    {
        LoadMenuPreviev();

        //previewManager.SpawnArenaByNum(levelCount);
        //previewManager.SpawnBossByNum(levelCount);
        //previewManager.SpawnPlayerByNum(levelCount);
    }
}
