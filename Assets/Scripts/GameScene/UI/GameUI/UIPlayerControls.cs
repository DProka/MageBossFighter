using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerControls : MonoBehaviour
{
    [SerializeField] UIPlayerButtonAttack attackButton;

    UIController controller;

    public void Init(UIController uIController)
    {
        controller = uIController;

    } 

    public void UpdateScript()
    {
        attackButton.UpdateScript();
    }

    public void SwitchPauseGame() { GameController.Instance.SwitchPauseGame(); }

    public void PlayerMoveLeft() { GameController.Instance.player.MoveLeft(); }
    public void PlayerMoveRight() { GameController.Instance.player.MoveRight(); }
    public void PlayerAttack() { GameController.Instance.player.Attack(); }
}
