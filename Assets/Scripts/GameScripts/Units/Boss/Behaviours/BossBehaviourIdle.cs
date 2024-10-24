
public class BossBehaviourIdle : IBehaviour
{
    private BossScript unit;

    public BossBehaviourIdle(BossScript thisUnit)
    {
        unit = thisUnit;
    }

    public void Enter()
    {
        unit.animationManager.PlayAnimation(BossAnimationManager.Anim.Idle);
    }

    void IBehaviour.Update()
    {
        
    }

    public void Exit()
    {
        
    }
}
