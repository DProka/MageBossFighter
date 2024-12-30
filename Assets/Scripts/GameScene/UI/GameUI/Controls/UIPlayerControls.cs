
using UnityEngine;

public class UIPlayerControls : MonoBehaviour
{
    [SerializeField] UIPlayerButtonAttack attackButton;
    [SerializeField] UIPlayerButton moveLeftButton;
    [SerializeField] UIPlayerButton moveRightButton;
    [SerializeField] UIJoyStick playerJoystick;

    [SerializeField] GameObject moveButtonsObj;
    [SerializeField] GameObject joystickObj;

    UIController controller;

    private bool isJoystick;
    private bool controlsIsActive;

    public void Init(UIController uIController)
    {
        controller = uIController;

        //playerJoystick.Init();

        SwitchMoveControls();
    }

    public void UpdateScript()
    {
        attackButton.UpdateScript();
        moveLeftButton.UpdateScript();
        moveRightButton.UpdateScript();
        playerJoystick.UpdateScript();
    }

    public void SwitchPauseGame() { GameController.Instance.SwitchPauseGame(); }

    public void PlayerMoveLeft()
    {
        if (controlsIsActive)
            GameController.Instance.player.MoveLeft();
    }

    public void PlayerMoveRight()
    {
        if (controlsIsActive)
            GameController.Instance.player.MoveRight();
    }

    public void PlayerAttack()
    {
        if (controlsIsActive)
            GameController.Instance.player.Attack();
    }

    public void SwitchMoveControls()
    {
        if (!isJoystick)
        {
            isJoystick = true;
            moveButtonsObj.SetActive(false);
            joystickObj.SetActive(true);
        }
        else
        {
            isJoystick = false;
            moveButtonsObj.SetActive(true);
            joystickObj.SetActive(false);
        }
    }

    public void SwitchControlsActive(bool isActive) => controlsIsActive = isActive;

}
