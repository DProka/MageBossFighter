using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : UnitGeneral
{
    public BossAnimationManager animationManager { get; private set; }
    public PlayerScript target { get; private set; }
    public bool isAlive { get; private set; }
    public bool isActive { get; private set; }

    public Transform _shootPoint => shootPoint;
    public EnemySettings _settings => settings;

    public float _currentHealth => currentHealth;

    [Header("Main")]

    [SerializeField] EnemySettings settings;

    [Header("Animations")]

    [SerializeField] Animator animator;

    [Header("Skills")]

    [SerializeField] Transform shootPoint;

    private BossBehaviourManager behaviourManager;
    private BossBehaviourManager.Behaviour currentBehaviour;

    public void Init(PlayerScript _target)
    {
        target = _target;
        animationManager = new BossAnimationManager(animator);
        behaviourManager = new BossBehaviourManager(this);
        behaviourManager.SetNewBehaviour(BossBehaviourManager.Behaviour.Idle);
        currentBehaviour = BossBehaviourManager.Behaviour.Idle;

        ResetEnemy();
    }

    public void EnemyUpdate()
    {
        if (isAlive)
        {
            behaviourManager.UpdateManager();

            Debug.Log("Boss is Updated");
        }

        animationManager.UpdateScript();
    }

    public override void GetHit(float damage)
    {
        if (isAlive)
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
    }

    public void ActivateBoss()
    {
        isActive = true;
        currentBehaviour = settings.startBehaviour;
        behaviourManager.SetNewBehaviour(currentBehaviour);
        Debug.Log("Boss is active");
    }

    public void SetRandomBehaviour()
    {
        List<BossBehaviourManager.Behaviour> newList = new List<BossBehaviourManager.Behaviour>();

        for (int i = 0; i < settings.skillsArray.Length; i++)
        {
            if (settings.skillsArray[i] != currentBehaviour)
                newList.Add(settings.skillsArray[i]);
        }

        int random = Random.Range(0, newList.Count);
        currentBehaviour = newList[random];
        behaviourManager.SetNewBehaviour(currentBehaviour);

        Debug.Log("BossBehaviour = " + currentBehaviour);
    }

    public void SpawnProjectile(Vector3 finishPos)
    {
        GameController.Instance.InstantiateProjectile(shootPoint.position, finishPos, false, settings.damage);
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
