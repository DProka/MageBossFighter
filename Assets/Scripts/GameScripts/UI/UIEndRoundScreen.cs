
using UnityEngine;
using TMPro;

public class UIEndRoundScreen : MonoBehaviour, IMenuScreen
{
    [SerializeField] TextMeshProUGUI endText;

    private Canvas mainCanvas;

    public void Init()
    {
        mainCanvas = GetComponent<Canvas>();

        SwitchMainCanvas(false);
    }

    public void CallScreen(bool win)
    {
        endText.text = win ? "YOU WIN" : "YOU LOSE";
        OpenScreen();
    }

    public void OpenScreen()
    {
        SwitchMainCanvas(true);
    }

    public void CloseScreen()
    {
        SwitchMainCanvas(false);
    }

    public void SwitchMainCanvas(bool isActive) => mainCanvas.enabled = isActive;
}
