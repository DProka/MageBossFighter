using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovePoint : MonoBehaviour
{
    public Status pointStatus;
    public float burningTime;
    public float freezeTime;
    public float blockedTime;

    public MeshRenderer meshRenderer;
    public Material startMaterial;
    public Material burnMaterial;
    public Material freezeMaterial;
    public Material blockedMaterial;

    private float burningTimer;
    private float freezeTimer;
    private float blockedTimer;

    void Start()
    {
        GetNewStatus(Status.NoStatus);
        burningTimer = burningTime;
        freezeTimer = freezeTime;
        blockedTimer = blockedTime;
    }

    void Update()
    {
        CheckStatus();
    }

    public void GetNewStatus(Status status)
    {
        if (status == Status.NoStatus)
        {
            pointStatus = Status.NoStatus;
            meshRenderer.material = startMaterial;
        }
        else if (status == Status.Burning)
        {
            pointStatus = Status.Burning;
            meshRenderer.material = burnMaterial;
        }
        else if (status == Status.Freeze)
        {
            pointStatus = Status.Freeze;
            meshRenderer.material = freezeMaterial;
        }
        else if (status == Status.Blocked)
        {
            pointStatus = Status.Blocked;
            meshRenderer.material = blockedMaterial;
        }
    }

    void CheckStatus()
    {
        switch (pointStatus)
        {
            case Status.Burning:
                burningTimer -= Time.deltaTime;
                if (burningTimer <= 0)
                {
                    burningTimer = burningTime;
                    GetNewStatus(Status.NoStatus);
                }
                break;
        
            case Status.Freeze:
                freezeTimer -= Time.deltaTime;
                if (freezeTimer <= 0)
                {
                    freezeTimer = freezeTime;
                    GetNewStatus(Status.NoStatus);
                }
                break;
        
            case Status.Blocked:
                blockedTimer -= Time.deltaTime;
                if (blockedTimer <= 0)
                {
                    blockedTimer = blockedTime;
                    GetNewStatus(Status.NoStatus);
                }
                break;
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
