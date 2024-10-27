
using UnityEngine;
using TMPro;

public class UIEndRoundScreen : MonoBehaviour
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
        mainCanvas.enabled = true;

        endText.text = win ? "YOU WIN" : "YOU LOSE";
    }

    public void SwitchMainCanvas(bool isActive) => mainCanvas.enabled = isActive;
}
