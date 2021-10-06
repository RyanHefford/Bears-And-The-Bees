using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class StatusEffectHandler : MonoBehaviour
{

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
        statusList.Add(effect);
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
        result.moveSpeed *= slowPercentage;
        return result;
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
}

public class InvisableStatus : StatusEffect
{

    public override PlayerStats ApplyEffect(PlayerStats playerStats)
    {
        throw new System.NotImplementedException();
    }
}

