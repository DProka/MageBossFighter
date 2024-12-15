
using UnityEngine;
using DG.Tweening;

public class MovePointPrefabScript : MonoBehaviour
{
    public int id { get; private set; }
    public Status pointStatus { get; private set; }

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] SpriteRenderer circleRenderer;

    private MovePointSettings settings;
    private Material currentMaterial;

    private float statusTimer;

    public void Init(MovePointSettings _settings, int _id)
    {
        settings = _settings;
        id = _id;
        currentMaterial = new Material(meshRenderer.material);
        meshRenderer.material = currentMaterial;

        SetNewStatus(Status.NoStatus);
        ResetAnimation();
    }

    public void UpdateScript()
    {
        CheckStatus();
    }

    public void SetPlayer()
    {
        if (pointStatus == Status.NoStatus)
            SetNewStatus(Status.Player);
    }

    public void SetNewStatus(Status status)
    {
        pointStatus = status;

        switch (status)
        {
            case Status.NoStatus:
                currentMaterial.color = settings.startColor;
                break;

            case Status.Burn:
                currentMaterial.color = settings.burnColor;
                statusTimer = settings.burningTime;
                break;

            case Status.Freeze:
                currentMaterial.color = settings.freezeColor;
                statusTimer = settings.freezeTime;
                break;

            case Status.Blocked:
                currentMaterial.color = settings.blockedColor;
                statusTimer = settings.blockedTime;
                break;
        
            case Status.Attack:
                //currentMaterial.color = settings.attackColor;
                statusTimer = settings.attackTime;
                StartAttackAnimation(2f);
                break;
        
            case Status.Player:
                break;
        }
    }

    public enum Status
    {
        NoStatus,
        Burn,
        Freeze,
        Blocked,
        Attack,

        Player
    }

    private void CheckStatus()
    {
        if (pointStatus != Status.NoStatus)
        {
            if (statusTimer > 0)
                statusTimer -= Time.deltaTime;
            else
                SetNewStatus(Status.NoStatus);
        }
    }

    #region Attack Animation

    private void StartAttackAnimation(float animTime)
    {
        circleRenderer.enabled = true;
        circleRenderer.transform.DOScale(1.5f, animTime).OnComplete(() => ResetAnimation());
    }

    private void ResetAnimation()
    {
        circleRenderer.enabled = false;
        circleRenderer.transform.DOScale(4f, 0);
    }

    #endregion
}