using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerInfo : MonoBehaviour
{
    [SerializeField] HealthBar playerHealthBar;
    [SerializeField] HealthBar playerComboBar;
    [SerializeField] TextMeshProUGUI comboText;
    //[SerializeField] UIPlayerControls playerControls;
    [SerializeField] UIPlayerStatusManager playerStatusManager;

    private UIController uIController;

    public void Init(UIController _uIController)
    {
        uIController = _uIController;

        //playerControls.Init(uIController);
        playerStatusManager.Init();

    }

    public void UpdateHealthBar(float maxHealth, float currentHealth) => playerHealthBar.SetFillAmount(maxHealth, currentHealth);

    public void UpdateComboBar(float maxValue, float currentValue)
    {
        playerComboBar.SetFillAmount(maxValue, currentValue);

        if(currentValue != 0)
            comboText.text = "Combo X" + currentValue;
        else
            comboText.text = "Combo";
    }

    #region Statuses

    public void UpdateStatus(PlayerStatus.Status type, float time) => playerStatusManager.UpdateStatus(type, time);

    public void SetStatusVisibility(PlayerStatus.Status type, bool isVisible) => playerStatusManager.SetStatusVisibility(type, isVisible);

    #endregion

}
