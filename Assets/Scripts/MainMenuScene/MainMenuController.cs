
using UnityEngine;

public class MainMenuController : MonoBehaviour 
{
    public static MainMenuController Instance;

    public int levelCount { get; private set; }

    public int playerCoins { get; private set; }
    public int playerXP { get; private set; }

    [SerializeField] MainMenuSettings settings;

    [Header("Preview Manager")]

    [SerializeField] MainMenuPreviewManager previewManager;

    [Header("UI Script")]

    [SerializeField] UIMenuController uiController;

    void Start()
    {
        Instance = this;

        levelCount = 0;
        playerCoins = 1000;

        if (DataHolder.playerCoins == 0)
            DataHolder.playerCoins = playerCoins;

        previewManager.Init(settings);
        uiController.Init(settings);

        CameraManager.Instance.SetCameraPosition(settings.cameraSettings);// new Vector3(0, 3.5f, -8), Quaternion.Euler(20, 0, 0), true);

        uiController.UpdatePlayerCoinsText(playerCoins);
        InitializePreview();
        GetPlayerCoins();
    }

    public void StartLevelByNum(int num)
    {
        ClearScene();
        DataHolder.gameLevel = num;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    public void CalculatePlayerCoins(int coins)
    {
        playerCoins += coins;

        if (playerCoins <= 0)
            playerCoins = 0;

        DataHolder.playerCoins = playerCoins;

        uiController.UpdatePlayerCoinsText(playerCoins);
    }
    
    public void GetPlayerCoins()
    {
        playerCoins = DataHolder.playerCoins;

        uiController.UpdatePlayerCoinsText(playerCoins);
    }

    #region Preview Part

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

    public EnemySettings GetEnemySettings() => previewManager.GetEnemySettings();

    private void InitializePreview()
    {
        //LoadMenuPreviev();

        //previewManager.SpawnArenaByNum(levelCount);
        //previewManager.SpawnBossByNum(levelCount);
        //previewManager.SpawnPlayerByNum(levelCount);
    }

    private void ClearScene()
    {
        previewManager.ClearManager();
    }

    #endregion
}
