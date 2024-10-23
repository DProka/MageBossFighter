
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public Status pointStatus { get; private set; }

    [SerializeField] MeshRenderer meshRenderer;

    private MovePointSettings settings;

    private float burningTimer;
    private float freezeTimer;
    private float blockedTimer;

    public void Init(MovePointSettings _settings)
    {
        settings = _settings;
        SetNewStatus(Status.NoStatus);
        burningTimer = settings.burningTime;
        freezeTimer = settings.freezeTime;
        blockedTimer = settings.blockedTime;
    }

    //void Update()
    //{
    //    CheckStatus();
    //}

    public void SetNewStatus(Status status)
    {
        switch (status)
        {
            case Status.NoStatus:
                pointStatus = Status.NoStatus;
                meshRenderer.material = settings.startMaterial;
                break;
        
            case Status.Burning:
                pointStatus = Status.Burning;
                meshRenderer.material = settings.burnMaterial;
                break;
        
            case Status.Freeze:
                pointStatus = Status.Freeze;
                meshRenderer.material = settings.freezeMaterial;
                break;
        
            case Status.Blocked:
                pointStatus = Status.Blocked;
                meshRenderer.material = settings.blockedMaterial;
                break;
        }
    }

    public void CheckStatus()
    {
        if(pointStatus == Status.Burning)
        {
            burningTimer -= Time.deltaTime;
            if(burningTimer <= 0)
            {
                burningTimer = settings.burningTime;
                SetNewStatus(Status.NoStatus);
            }
        }
        else if (pointStatus == Status.Freeze)
        {
            freezeTimer -= Time.deltaTime;
            if(freezeTimer <= 0)
            {
                freezeTimer = settings.freezeTime;
                SetNewStatus(Status.NoStatus);
            }
        }
        else if (pointStatus == Status.Blocked)
        {
            blockedTimer -= Time.deltaTime;
            if(blockedTimer <= 0)
            {
                blockedTimer = settings.blockedTime;
                SetNewStatus(Status.NoStatus);
            }
        }
    }

    public enum Status
    {
        NoStatus,
        Burning,
        Freeze,
        Blocked
    }
}