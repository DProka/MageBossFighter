
using UnityEngine;

public class UIStartGameScript : MonoBehaviour, IMenuScreen
{
    [Header("Map Part")]

    [SerializeField] GameObject mapObject;
    [SerializeField] Transform lvlButtonsParent;

    [Header("Level Part")]

    [SerializeField] GameObject levelObject;

    private Canvas mainCanvas;
    private UIMenuController uiController;
    private int screenNum;

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
        mapObject.SetActive(false);
        levelObject.SetActive(true);
        MainMenuController.Instance.LoadLevelPreviev(num);

        Debug.Log("Level was select " + num);
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
