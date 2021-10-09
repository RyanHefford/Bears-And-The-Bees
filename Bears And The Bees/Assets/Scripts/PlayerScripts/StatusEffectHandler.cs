using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class StatusEffectHandler : MonoBehaviour
{
    private StatusEffectIcons iconHandle;

    private void Start()
    {
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("PlayerHUD");
        foreach (GameObject hudObject in tempList)
        {
            if (hudObject.GetComponent<StatusEffectIcons>())
            {
                iconHandle = hudObject.GetComponent<StatusEffectIcons>();
                break;
            }
        }
    }

    private List<StatusEffect> statusList = new List<StatusEffect>();


    public PlayerStats ApplyStatusEffects(PlayerStats initalStats)
    {
        PlayerStats result = initalStats;


        foreach (StatusEffect currStatusEffect in statusList.ToArray())
        {
            currStatusEffect.UpdateTimer();
            if (currStatusEffect.durationLeft > 0)
            {
                result = currStatusEffect.ApplyEffect(result);
            }
            else
            {
                statusList.Remove(currStatusEffect);
            }
        }

        return result;
    }

    public void AddStatus(StatusEffect effect)
    {
        if (!CheckForExisting(effect))
        {
            statusList.Add(effect);
            iconHandle.AddStatusIcon(effect);
        }
    }


    private bool CheckForExisting(StatusEffect effect)
    {
        foreach (StatusEffect currEffect in statusList)
        {
            if (currEffect.GetType().Name == effect.GetType().Name)
            {
                UpdateExisting(effect, currEffect);
                return true;
            }
        }
        return false;
    }

    private void UpdateExisting(StatusEffect newEffect, StatusEffect oldEffect)
    {
        oldEffect.UpdateEffect(newEffect);
        iconHandle.UpdateExisting(newEffect);

    }
}



//---------------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------
public abstract class StatusEffect : ScriptableObject
{
    public float durationLeft;
    public GameObject player;
    public void Init(float _duration)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdateTimer()
    {
        durationLeft -= Time.deltaTime;
    }

    public abstract PlayerStats ApplyEffect(PlayerStats playerStats);

    public abstract void UpdateEffect(StatusEffect newEffect);
}


//---------------------------------------------------------------------------------------------------------------------------------

public class SlowStatus : StatusEffect
{
    private float slowPercentage;
    public void Init(float _duration, float _slowPercentage)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        slowPercentage = _slowPercentage;
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.moveSpeed *= 1 - slowPercentage;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        SlowStatus update = (SlowStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
        slowPercentage = update.slowPercentage > slowPercentage ? update.slowPercentage : slowPercentage;
    }
}


//---------------------------------------------------------------------------------------------------------------------------------

public class SpeedStatus : StatusEffect
{
    private float speedUpPercentage;
    public void Init(float _duration, float _speedUpPercentage)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        speedUpPercentage = _speedUpPercentage;
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.moveSpeed *= 1 + speedUpPercentage;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        SpeedStatus update = (SpeedStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
        speedUpPercentage = update.speedUpPercentage > speedUpPercentage ? update.speedUpPercentage : speedUpPercentage;
    }
}


//---------------------------------------------------------------------------------------------------------------------------------

public class InvisibleStatus : StatusEffect
{

    public new void Init(float _duration)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<ParticleSystem>().Play();
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.isInvisible = true;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        InvisibleStatus update = (InvisibleStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
    }
}

//---------------------------------------------------------------------------------------------------------------------------------

public class JumpUpStatus : StatusEffect
{
    private float jumpUpPercent;
    public void Init(float _duration, float _jumpUpPercent)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        jumpUpPercent = _jumpUpPercent;
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.jumpSpeed *= 1 + jumpUpPercent;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        JumpUpStatus update = (JumpUpStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
        jumpUpPercent = update.jumpUpPercent > jumpUpPercent ? update.jumpUpPercent : jumpUpPercent;
    }
}

//---------------------------------------------------------------------------------------------------------------------------------

public class JumpDownStatus : StatusEffect
{
    private float jumpDownPercent;
    public void Init(float _duration, float _jumpDownPercent)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        jumpDownPercent = _jumpDownPercent;
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.jumpSpeed *= 1 - jumpDownPercent;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        JumpDownStatus update = (JumpDownStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
        jumpDownPercent = update.jumpDownPercent > jumpDownPercent ? update.jumpDownPercent : jumpDownPercent;
    }
}

//---------------------------------------------------------------------------------------------------------------------------------

public class NoiseUpStatus : StatusEffect
{
    private float noiseUpPercent;
    public void Init(float _duration, float _noiseUpPercent)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        noiseUpPercent = _noiseUpPercent;
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.noiseMultiplier *= 1 + noiseUpPercent;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        NoiseUpStatus update = (NoiseUpStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
        noiseUpPercent = update.noiseUpPercent > noiseUpPercent ? update.noiseUpPercent : noiseUpPercent;
    }
}

//---------------------------------------------------------------------------------------------------------------------------------

public class NoiseDownStatus : StatusEffect
{
    private float noiseDownPercent;
    public void Init(float _duration, float _noiseDownPercent)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        noiseDownPercent = _noiseDownPercent;
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.noiseMultiplier *= 1 - noiseDownPercent;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        NoiseDownStatus update = (NoiseDownStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
        noiseDownPercent = update.noiseDownPercent > noiseDownPercent ? update.noiseDownPercent : noiseDownPercent;
    }
}

//---------------------------------------------------------------------------------------------------------------------------------

public class StealthUpStatus : StatusEffect
{
    private float stealthUpPercent;
    public void Init(float _duration, float _stealthUpPercent)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        stealthUpPercent = _stealthUpPercent;
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.visibility *= 1 - stealthUpPercent;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        StealthUpStatus update = (StealthUpStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
        stealthUpPercent = update.stealthUpPercent > stealthUpPercent ? update.stealthUpPercent : stealthUpPercent;
    }
}

//---------------------------------------------------------------------------------------------------------------------------------

public class StealthDownStatus : StatusEffect
{
    private float stealthDownPercent;
    public void Init(float _duration, float _stealthDownPercent)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
        stealthDownPercent = _stealthDownPercent;
    }

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        PlayerStats result = playerStats;
        result.visibility *= 1 + stealthDownPercent;
        return result;
    }

    public override void UpdateEffect(StatusEffect newEffect)
    {
        StealthDownStatus update = (StealthDownStatus)newEffect;

        durationLeft = update.durationLeft > durationLeft ? update.durationLeft : durationLeft;
        stealthDownPercent = update.stealthDownPercent > stealthDownPercent ? update.stealthDownPercent : stealthDownPercent;
    }
}