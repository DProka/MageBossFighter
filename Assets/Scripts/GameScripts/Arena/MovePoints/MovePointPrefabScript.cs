
using UnityEngine;

public class MovePointPrefabScript : MonoBehaviour
{
    public Status pointStatus { get; private set; }

    [SerializeField] MeshRenderer meshRenderer;

    private MovePointSettings settings;

    private float statusTimer;

    public void Init(MovePointSettings _settings)
    {
        settings = _settings;
        SetNewStatus(Status.NoStatus);
    }

    public void UpdateScript()
    {
        CheckStatus();
    }

    public void SetNewStatus(Status status)
    {
        pointStatus = status;

        switch (status)
        {
            case Status.NoStatus:
                meshRenderer.material = settings.startMaterial;
                break;

            case Status.Burning:
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
        Burning,
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