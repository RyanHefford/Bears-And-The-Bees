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

