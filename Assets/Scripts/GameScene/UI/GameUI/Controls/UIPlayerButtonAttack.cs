
public class UIPlayerButtonAttack : UIPlayerButton
{
    public override void ButtonWorks()
    {
        base.ButtonWorks();

        GameController.Instance.player.Attack();
    }
}
