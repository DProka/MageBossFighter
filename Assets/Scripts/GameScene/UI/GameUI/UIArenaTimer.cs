using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIArenaTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    public void UpdateTimer(float timer)
    {
        if (timerText.enabled)
        {
            int time = Mathf.FloorToInt(timer);

            if (time > 0)
                timerText.text = "" + time;
            else
                timerText.text = "GO";
        }
    }

    public void SwitchTimerActive(bool isActive) { timerText.enabled = isActive; }
}
