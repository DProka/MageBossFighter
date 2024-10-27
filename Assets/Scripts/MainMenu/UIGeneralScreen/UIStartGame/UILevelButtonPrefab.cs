
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevelButtonPrefab : MonoBehaviour
{
    [SerializeField] Image backImage;
    [SerializeField] TextMeshProUGUI levelText;

    private UIStartGameScript menuScript;
    private int lvlNum;

    public void Init(UIStartGameScript _menuScript, int _lvlNum)
    {
        menuScript = _menuScript;
        lvlNum = _lvlNum;
        levelText.text = "Level " + lvlNum;
    }

    public void ChooseCurrentLevel() { menuScript.OpenSelectedLevel(lvlNum); }
}
