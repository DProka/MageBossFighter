using System.Collections;
using UnityEngine;

public class BossScript : UnitGeneral
{
    public BossAnimationManager animationManager { get; private set; }
    public PlayerScript target { get; private set; }
    public bool isAlive { get; private set; }
    public bool isActive { get; private set; }

    public Transform _shootPoint => shootPoint;
    public EnemySettings _settings => settings;

    [Header("Main")]

    [SerializeField] EnemySettings settings;

    [Header("Animations")]

    [SerializeField] Animator animator;

    [Header("Skills")]

    [SerializeField] Transform shootPoint;

    private BossBehaviourManager behaviourManager;

    public void Init(PlayerScript _target)
    {
        target = _target;
        animationManager = new BossAnimationManager(animator);
        behaviourManager = new BossBehaviourManager(this);
        behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.Idle);

        ResetEnemy();
    }

    public void EnemyUpdate()
    {
        if (isAlive)
        {
            behaviourManager.UpdateManager();
        }

        animationManager.UpdateScript();
    }

    public override void GetHit(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animationManager.PlayAnimation(BossAnimationManager.Anim.GetHit);
        }
        else
        {
            currentHealth = 0;
            SetDeath();
        }

        UIController.Instance.UpdateHealthBar(false, settings.maxHealth, currentHealth);
    }

    public void ActivateBoss()
    {
        isActive = true;
        
        behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.SimpleAttack);
        //behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.RoundAttack);
        //behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.SpiralAttack);
        //behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.EvenOddAttack);
        //behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.BurnMovepoint);
        //behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.FreezeMovepoint);
        //behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.BlockMovepoint);
        
        Debug.Log("Boss is active");
    }

    public void SetRandomBehaviour()
    {
        int random = Random.Range(0, settings.skillsArray.Length);
        behaviourManager.SetNewBehaviour(settings.skillsArray[random]);
    }

    public void ResetEnemy()
    {
        currentHealth = settings.maxHealth;
        UIController.Instance.UpdateHealthBar(false, settings.maxHealth, currentHealth);
        behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.Idle);
        isAlive = true;
    }

    private void SetDeath()
    {
        currentHealth = 0;
        isAlive = false;
        isActive = false;

        animationManager.PlayAnimation(BossAnimationManager.Anim.Death);
        GameEventBus.OnSomeoneDies?.Invoke();
    }
}
