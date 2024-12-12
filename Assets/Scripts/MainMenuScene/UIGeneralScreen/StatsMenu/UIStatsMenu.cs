using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatsMenu : MonoBehaviour
{
    [SerializeField] Transform statsParent;
    [SerializeField] UIStatPrefab[] statsPrefArray;

    private Canvas mainCanvas;
    private UIMenuController menuController;
    private int[] statsLvls;

    public void Init(UIMenuController _menuController)
    {
        mainCanvas = transform.GetComponent<Canvas>();
        mainCanvas.enabled = false;

        menuController = _menuController;

        InitializeStats();
    }

    public void UpgradeStat(int num)
    {
        int priceToUpgrade = 50 * DataHolder.statsLvls[num];

        if(CheckStatPrice(num))
        {
            MainMenuController.Instance.CalculatePlayerCoins(-priceToUpgrade);
            DataHolder.statsLvls[num] += 1;
            statsPrefArray[num].UpdateStat(DataHolder.statsLvls[num]);
            statsPrefArray[num].SwitchButtonActive(CheckStatPrice(num));
        }

        ChechStatsAccess();
    }

    public enum StatType
    {
        Health,
        Damage,
        AttackSpeed
    }

    private bool CheckStatPrice(int num)
    {
        int priceToUpgrade = 50 * DataHolder.statsLvls[num];

        return DataHolder.playerCoins >= priceToUpgrade;
    }

    private void ChechStatsAccess()
    {
        for (int i = 0; i < statsParent.childCount; i++)
        {
            statsPrefArray[i].SwitchButtonActive(CheckStatPrice(i));
        }
    }

    private void InitializeStats()
    {
        string[] statsNames = new string[] { "Health Points", "Damage", "Attack Speed" };
        statsLvls = new int[statsNames.Length];

        if (DataHolder.statsLvls == null)
        {
            DataHolder.statsLvls = statsLvls;

            for (int i = 0; i < statsLvls.Length; i++)
            {
                statsLvls[i] = 1;
            }
        }
        else
        {
            statsLvls = DataHolder.statsLvls;
        }

        for (int i = 0; i < statsParent.childCount; i++)
        {
            statsPrefArray[i].SetStat(this, i, statsNames[i], statsLvls[i]);
            statsPrefArray[i].SwitchButtonActive(CheckStatPrice(i));
        }
    }

    #region Window

    public void OpenMenu()
    {
        mainCanvas.enabled = true;
    }

    public void CloseMenu()
    {
        //menuController.OpenScreen(UIMenuController.Screen.MainMenu);
        mainCanvas.enabled = false;
    }

    #endregion
}
