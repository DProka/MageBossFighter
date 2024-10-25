
using UnityEngine;

public class PlayerStatus
{
    public bool isBurn { get; private set; }
    public bool isFreeze { get; private set; }

    private PlayerScript player;
    private PlayerSettings settings;

    private float burnStatusTimer;
    private float burnDamageTimer;

    private float freezeStatusTimer;
    private float freezeDamageTimer;

    public PlayerStatus(PlayerScript _player, PlayerSettings _settings)
    {
        player = _player;
        settings = _settings;

        isBurn = false;
        isFreeze = false;
    }

    public void UpdateScript()
    {
        UpdateBurn();
        UpdateFreeze();
    }

    public void SetStatus(Status newStatus)
    {
        Debug.Log("NewStatus = " + newStatus);

        switch (newStatus)
        {
            case Status.NoStatus:
                isBurn = false;
                isFreeze = false;
                break;

            case Status.Burn:
                burnStatusTimer = settings.burnStatusTime;
                burnDamageTimer = settings.burnDamageTime;
                isBurn = true;
                break;
        
            case Status.Freeze:
                freezeStatusTimer = settings.freezeStatusTime;
                freezeDamageTimer = settings.freezeDamageTime;
                isFreeze = true;
                break;
        }
    }

    public enum Status
    {
        NoStatus,
        Burn,
        Freeze
    }

    private void UpdateBurn()
    {
        if (isBurn)
        {
            if (burnStatusTimer > 0)
            {
                burnStatusTimer -= Time.deltaTime;

                if (burnDamageTimer > 0)
                    burnDamageTimer -= Time.deltaTime;
                else
                    GetBurnHit();
            }
            else
                isBurn = false;
        }
    }

    private void GetBurnHit()
    {
        player.GetHit(settings.burnDamage);
        burnDamageTimer = settings.burnDamageTime;
    }

    private void UpdateFreeze()
    {
        if (isFreeze)
        {
            if (freezeStatusTimer > 0)
            {
                freezeStatusTimer -= Time.deltaTime;

                if (freezeDamageTimer > 0)
                    freezeDamageTimer -= Time.deltaTime;
                else
                    GetFreezeHit();
            }
            else
                isBurn = false;
        }
    }

    private void GetFreezeHit()
    {
        player.GetHit(settings.freezeDamage);
        freezeDamageTimer = settings.freezeDamageTime;
    }
}
