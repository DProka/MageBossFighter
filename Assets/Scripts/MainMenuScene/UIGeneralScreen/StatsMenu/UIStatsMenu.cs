using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatsMenu : MonoBehaviour
{
    [SerializeField] Transform statsParent;

    private Canvas mainCanvas;
    private UIMenuController menuController;
    [SerializeField] UIStatPrefab[] statsPrefArray;

    public void Init(UIMenuController _menuController)
    {
        mainCanvas = transform.GetComponent<Canvas>();
        mainCanvas.enabled = false;

        menuController = _menuController;

        InitializeStats();
    }

    public void OpenMenu()
    {
        menuController.OpenScreen(UIMenuController.Screen.StatsMenu);
    }

    public void CloseMenu()
    {
        //menuController.OpenScreen(UIMenuController.Screen.MainMenu);
        mainCanvas.enabled = false;
    }

    private void InitializeStats()
    {
        string[] statsNames = new string[] { "Health Points", "Damage", "Attack Speed" };

        //statsPrefArray = new UIStatPrefab[statsParent.childCount];

        for (int i = 0; i < statsParent.childCount; i++)
        {
            //statsPrefArray[i] = statsParent.GetComponent<UIStatPrefab>();
            statsPrefArray[i].SetStat(statsNames[i], 0, 1);
            statsPrefArray[i].SwitchButtonActive(true);
        }
    }
}
