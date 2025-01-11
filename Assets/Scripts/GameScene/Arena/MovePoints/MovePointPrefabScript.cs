
using UnityEngine;

public class MovePointPrefabScript : MonoBehaviour
{
    public int id { get; private set; }
    public ArenaManager.PointStatus currentStatus { get; private set; }

    [SerializeField] MeshRenderer meshRenderer;

    [Header("Visualization")]

    [SerializeField] MovePointCirclePrefab circlePrefab;
    [SerializeField] VfxMovePointPrefab vfxFreeze;

    private MovePointSettings settings;
    private Material currentMaterial;

    private IMovePointBehaviour currentBehaviour;

    public void Init(MovePointSettings _settings, int _id)
    {
        settings = _settings;
        id = _id;

        currentMaterial = new Material(meshRenderer.material);
        meshRenderer.material = currentMaterial;

        //SetNewStatus(ArenaManager.PointStatus.NoStatus);
        ResetVfx();
        circlePrefab.Init(settings);
    }

    public void UpdateScript()
    {
        UpdateBehaviour();
    }

    public void SetPlayer()
    {
        if (currentStatus == ArenaManager.PointStatus.NoStatus)
            SetNewStatus(ArenaManager.PointStatus.Player);
    }

    #region Statuses

    public void SetNewBehaviour(ArenaManager.PointStatus newStatus, IMovePointBehaviour newBehaviour)
    {
        currentBehaviour = null;
        currentStatus = newStatus;

        if (currentBehaviour != newBehaviour)
        {
            if (currentBehaviour != null)
                currentBehaviour.Exit();

            currentBehaviour = newBehaviour;
            currentBehaviour.Enter(this);
        }
    }

    private void UpdateBehaviour()
    {
        if(currentBehaviour != null && currentStatus != ArenaManager.PointStatus.NoStatus)
            currentBehaviour.Update();
    }

    public void SetNewStatus(ArenaManager.PointStatus status)
    {
        currentStatus = status;

        switch (status)
        {
            case ArenaManager.PointStatus.NoStatus:
                //StopAllVfx();
                //currentMaterial.color = settings.cleanColor;
                break;

            case ArenaManager.PointStatus.Burn:
                //currentMaterial.color = settings.burnColor;
                //statusTimer = settings.burningTime;
                break;

            case ArenaManager.PointStatus.Freeze:
                //currentMaterial.color = settings.freezeColor;
                //statusTimer = settings.freezeTime;
                break;

            case ArenaManager.PointStatus.Blocked:
                //currentMaterial.color = settings.blockedColor;
                //statusTimer = settings.blockedTime;
                break;
        
            case ArenaManager.PointStatus.Attack:
                //currentMaterial.color = settings.attackColor;
                //statusTimer = settings.attackTime;
                //circlePrefab.StartAttackAnimation(2f);
                break;
        
            case ArenaManager.PointStatus.Player:
                break;
        }
    }

    //public void ResetStatus()
    //{
    //    SetNewBehaviour(ArenaManager.PointStatus.NoStatus, )
    //}

    #endregion

    #region Visual

    public void SetVfxByStatus(ArenaManager.PointStatus status)
    {
        ResetVfx();

        switch (status)
        {
            case ArenaManager.PointStatus.NoStatus:
                break;
        
            case ArenaManager.PointStatus.Player:
                break;
        
            case ArenaManager.PointStatus.Freeze:
                vfxFreeze.PalyVfx();
                currentMaterial.color = settings.attentionColor;
                break;
        }

        circlePrefab.SetStatus(status);
    }

    public void ResetVfx()
    {
        currentMaterial.color = settings.cleanColor;
        circlePrefab.ResetAnimation();

        vfxFreeze.StopVfx();
    }

    #endregion
}