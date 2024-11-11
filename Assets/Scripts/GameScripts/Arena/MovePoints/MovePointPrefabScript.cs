
using UnityEngine;

public class MovePointPrefabScript : MonoBehaviour
{
    public int id { get; private set; }
    public Status pointStatus { get; private set; }

    [SerializeField] MeshRenderer meshRenderer;

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
                currentMaterial.color = settings.attackColor;
                statusTimer = settings.attackTime;
                break;
        
            case Status.Player:
                break;
        }

        //meshRenderer.material = currentMaterial;
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
}