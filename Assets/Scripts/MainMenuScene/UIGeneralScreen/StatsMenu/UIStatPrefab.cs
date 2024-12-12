
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatPrefab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statLevelText;
    [SerializeField] TextMeshProUGUI statCountText;
    [SerializeField] TextMeshProUGUI statPriceText;

    [Header("Button")]

    [SerializeField] Image buttonImg;
    [SerializeField] Color[] colorsArray;

    private UIStatsMenu menu;
    private int id;

    public void SetStat(UIStatsMenu _menu, int _id, string _name, int lvl)
    {
        menu = _menu;
        id = _id;
        statNameText.text = _name;
        UpdateStat(lvl);
    }

    public void UpgradeStat() => menu.UpgradeStat(id);

    public void UpdateStat(int level)
    {
        statLevelText.text = "Stat Level : " + level;
        statCountText.text = "Power += " + level + "%";
        statPriceText.text = "" + (level * 50);
    }

    public void SwitchButtonActive(bool isActive)
    {
        if (isActive)
            buttonImg.color = colorsArray[1];
        else
            buttonImg.color = colorsArray[0];
    }
}
