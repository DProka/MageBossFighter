
public class BossBehaviourIdle : IBossBehaviour
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

    void IBossBehaviour.Update()
    {
        
    }

    public void Exit()
    {
        
    }
}
