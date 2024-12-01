
using UnityEngine;
using TMPro;

public class UIStartGameScript : MonoBehaviour, IMenuScreen
{
    [Header("Map Part")]

    [SerializeField] GameObject mapObject;
    [SerializeField] Transform lvlButtonsParent;

    [Header("Level Part")]

    [SerializeField] GameObject levelObject;
    [SerializeField] TextMeshProUGUI levelNumberText;
    [SerializeField] TextMeshProUGUI bossNameText;

    private Canvas mainCanvas;
    private UIMenuController uiController;
    private int screenNum;
    private int selectedLvlNum;

    private UILevelButtonPrefab[] lvlButtonsArray;

    public void Init(UIMenuController _uiController)
    {
        mainCanvas = GetComponent<Canvas>();
        uiController = _uiController;

        InitializeScreen();
    }

    public void OpenSelectedLevel(int num)
    {
        screenNum = 1;
        selectedLvlNum = num;
        mapObject.SetActive(false);
        levelObject.SetActive(true);
        MainMenuController.Instance.LoadLevelPreviev(num);

        levelNumberText.text = "Level " + (num + 1);

        EnemySettings enemy = MainMenuController.Instance.GetEnemySettings();

        if(enemy != null)
        {
            bossNameText.text = enemy.bossName;
        }

        Debug.Log("Level was select " + (num + 1));
    }

    public void GoBack()
    {
        switch (screenNum)
        {
            case 0:
                uiController.OpenScreen(UIMenuController.Screen.MainMenu);
                break;

            case 1:
                OpenScreen();
                break;
        }
    }

    public void StartLevelByNum() => MainMenuController.Instance.StartLevelByNum(selectedLvlNum);
    
    private void InitializeScreen()
    {
        lvlButtonsArray = new UILevelButtonPrefab[lvlButtonsParent.childCount];

        for (int i = 0; i < lvlButtonsParent.childCount; i++)
        {
            lvlButtonsArray[i] = lvlButtonsParent.GetChild(i).GetComponent<UILevelButtonPrefab>();
            lvlButtonsArray[i].Init(this, i);
        }
    }

    #region Screen

    public void OpenScreen()
    {
        screenNum = 0;
        mapObject.SetActive(true);
        levelObject.SetActive(false);

        SwitchActive(true);
    }

    public void CloseScreen()
    {
        SwitchActive(false);
    }

    private void SwitchActive(bool isActive) => mainCanvas.enabled = isActive;

    #endregion
}
