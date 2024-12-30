using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStatusPrefab : MonoBehaviour
{
    [SerializeField] Image statusImage;
    [SerializeField] TextMeshProUGUI statusTimerText;

    public void UpdateStatusTimer(float timer)
    {
        if (timer > 0.1)
        {
            if (!statusImage.enabled)
                SetTimerVisible(true);

            statusTimerText.text = "" + (int)timer;
        }
    }

    public void SetTimerVisible(bool isVisible)
    {
        statusImage.enabled = isVisible;
        statusTimerText.enabled = isVisible;
    }
}
