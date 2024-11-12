using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStatusManager : MonoBehaviour
{
    [SerializeField] UIPlayerStatusPrefab[] playerStatusArray;

    public void Init()
    {
        foreach (UIPlayerStatusPrefab status in playerStatusArray)
            status.SetTimerVisible(false);
    }

    public void UpdateStatus(PlayerStatus.Status type, float time)
    {
        switch (type)
        {
            case PlayerStatus.Status.Burn:
                playerStatusArray[0].UpdateStatusTimer(time);
                break;

            case PlayerStatus.Status.Freeze:
                playerStatusArray[1].UpdateStatusTimer(time);
                break;
        }
    }
    
    public void SetStatusVisibility(PlayerStatus.Status type, bool isVisible)
    {
        switch (type)
        {
            case PlayerStatus.Status.Burn:
                playerStatusArray[0].SetTimerVisible(isVisible);
                break;

            case PlayerStatus.Status.Freeze:
                playerStatusArray[1].SetTimerVisible(isVisible);
                break;
        }
    }
}
