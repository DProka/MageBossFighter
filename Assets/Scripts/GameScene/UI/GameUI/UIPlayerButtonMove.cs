
public class UIPlayerButtonMove : UIPlayerButton
{
    public bool isLeft;

    public override void ButtonWorks()
    {
        base.ButtonWorks();

        if(isLeft)
            GameController.Instance.player.MoveLeft();
        else
            GameController.Instance.player.MoveRight();
    }
}
