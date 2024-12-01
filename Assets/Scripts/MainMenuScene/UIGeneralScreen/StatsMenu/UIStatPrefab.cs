
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatPrefab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statLevelText;
    [SerializeField] TextMeshProUGUI statCountText;

    [Header("Button")]

    [SerializeField] Image buttonImg;
    [SerializeField] Color[] colorsArray;

    public void SetStat(string _name, int lvl, float count)
    {
        statNameText.text = _name;
        UpdateStat(lvl, count);
    }

    public void UpdateStat(int level, float count)
    {
        statLevelText.text = "Stat Level : " + level;
        statCountText.text = "Power += " + count + "%";
    }

    public void SwitchButtonActive(bool isActive)
    {
        if (isActive)
            buttonImg.color = colorsArray[1];
        else
            buttonImg.color = colorsArray[0];
    }
}
