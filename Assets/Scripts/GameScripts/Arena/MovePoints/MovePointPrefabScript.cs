
using UnityEngine;

public class MovePointPrefabScript : MonoBehaviour
{
    public int id { get; private set; }
    public Status pointStatus { get; private set; }

    [SerializeField] MeshRenderer meshRenderer;

    private MovePointSettings settings;

    private float statusTimer;

    public void Init(MovePointSettings _settings, int _id)
    {
        settings = _settings;
        id = _id;
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
                meshRenderer.material = settings.startMaterial;
                break;

            case Status.Burn:
                meshRenderer.material = settings.burnMaterial;
                statusTimer = settings.burningTime;
                break;

            case Status.Freeze:
                meshRenderer.material = settings.freezeMaterial;
                statusTimer = settings.freezeTime;
                break;

            case Status.Blocked:
                meshRenderer.material = settings.blockedMaterial;
                statusTimer = settings.blockedTime;
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