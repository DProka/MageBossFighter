
using UnityEngine;

public class MovePointPrefabScript : MonoBehaviour
{
    public int id { get; private set; }
    public ArenaManager.PointStatus currentStatus { get; private set; }
    public BoosterManager.Booster currentBooster { get; private set; }

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] MovepointCirclePrefab circlePrefab;

    private MovePointSettings settings;
    private Material currentMaterial;

    private float statusTimer;

    private float boosterTimer;

    public void Init(MovePointSettings _settings, int _id)
    {
        settings = _settings;
        id = _id;
        //currentMaterial = new Material(meshRenderer.material);
        //meshRenderer.material = currentMaterial;

        SetNewStatus(ArenaManager.PointStatus.NoStatus);
        circlePrefab.Init(settings);
    }

    public void UpdateScript()
    {
        CheckStatus();
        UpdateBoosterTimer();
    }

    public void SetPlayer()
    {
        if (currentStatus == ArenaManager.PointStatus.NoStatus)
            SetNewStatus(ArenaManager.PointStatus.Player);
    }

    #region Statuses

    public void SetNewStatus(ArenaManager.PointStatus status)
    {
        currentStatus = status;

        switch (status)
        {
            case ArenaManager.PointStatus.NoStatus:
                //currentMaterial.color = settings.startColor;
                break;

            case ArenaManager.PointStatus.Burn:
                //currentMaterial.color = settings.burnColor;
                statusTimer = settings.burningTime;
                break;

            case ArenaManager.PointStatus.Freeze:
                //currentMaterial.color = settings.freezeColor;
                statusTimer = settings.freezeTime;
                break;

            case ArenaManager.PointStatus.Blocked:
                //currentMaterial.color = settings.blockedColor;
                statusTimer = settings.blockedTime;
                break;
        
            case ArenaManager.PointStatus.Attack:
                //currentMaterial.color = settings.attackColor;
                statusTimer = settings.attackTime;
                circlePrefab.StartAttackAnimation(2f);
                break;
        
            case ArenaManager.PointStatus.Player:
                break;
        }
    }

    //public enum Status
    //{
    //    NoStatus,
    //    Burn,
    //    Freeze,
    //    Blocked,
    //    Attack,

    //    Player
    //}

    private void CheckStatus()
    {
        if (currentStatus != ArenaManager.PointStatus.NoStatus)
        {
            if (statusTimer > 0)
                statusTimer -= Time.deltaTime;
            else
                SetNewStatus(ArenaManager.PointStatus.NoStatus);
        }
    }

    #endregion

    #region Boosters

    public void SetBooster(BoosterManager.Booster booster)
    {
        currentBooster = booster;

        switch (booster)
        {
            case BoosterManager.Booster.Health:
                boosterTimer = settings.healthTime;
                break;
        
            case BoosterManager.Booster.AttackDamage:
                boosterTimer = settings.attackDamageTime;
                break;
        
            case BoosterManager.Booster.AttackSpeed:
                boosterTimer = settings.attackSpeedTime;
                break;
        
            case BoosterManager.Booster.Defence:
                boosterTimer = settings.defenceTime;
                break;
        }
    }

    private void UpdateBoosterTimer()
    {
        if(currentBooster != BoosterManager.Booster.Empty)
        {
            if (boosterTimer > 0)
                boosterTimer -= Time.deltaTime;
            else
                ResetBooster();
        }
    }

    private void ResetBooster()
    {
        currentBooster = BoosterManager.Booster.Empty;
    }

    #endregion

}