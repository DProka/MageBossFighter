
using UnityEngine;

public class UIPlayerInfo : MonoBehaviour
{
    [SerializeField] HealthBar playerHealthBar;
    [SerializeField] PlayerComboPart comboPart;
    //[SerializeField] UIPlayerControls playerControls;
    [SerializeField] UIPlayerStatusManager playerStatusManager;

    private UIController uIController;

    public void Init(UIController _uIController)
    {
        uIController = _uIController;

        //playerControls.Init(uIController);
        playerStatusManager.Init();
        comboPart.Init();
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth) => playerHealthBar.SetFillAmount(maxHealth, currentHealth);

    public void UpdateComboBar(float maxValue, float currentValue) => comboPart.UpdateComboBar(maxValue, currentValue);

    #region Statuses

    public void UpdateStatus(PlayerStatus.Status type, float time) => playerStatusManager.UpdateStatus(type, time);

    public void SetStatusVisibility(PlayerStatus.Status type, bool isVisible) => playerStatusManager.SetStatusVisibility(type, isVisible);

    #endregion

}
