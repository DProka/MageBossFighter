using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseGameScreen : MonoBehaviour
{
    private Canvas mainCanvas;

    public void Init()
    {
        mainCanvas = GetComponent<Canvas>();
        mainCanvas.enabled = false;
    }

    public void SwitchPauseGame(bool isPaused) => mainCanvas.enabled = isPaused;

}
