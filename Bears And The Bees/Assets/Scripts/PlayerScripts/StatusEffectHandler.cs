using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{

    private List<StatusEffect> statusList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (StatusEffect statusEffect in statusList)
        {
            statusEffect.ApplyEffect();
        }
    }

    public void AddStatus(StatusEffect effect)
    {
        statusList.Add(effect);
    }
}


//--------------------------------------------------

public abstract class StatusEffect : MonoBehaviour
{
    public float durationLeft;
    public GameObject player;
    public StatusEffect(float _duration)
    {
        durationLeft = _duration;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        durationLeft -= Time.deltaTime;
    }

    public abstract void ApplyEffect();
}

public class SlowStatus : StatusEffect
{
    private float slowPercentage;
    public SlowStatus(float _duration, float _slowPercentage) : base(_duration)
    {
        slowPercentage = _slowPercentage;
    }

    public override void ApplyEffect()
    {
        player.GetComponent<PlayerMovement>().currentStats.moveSpeed *= slowPercentage;
    }
}

public class InvisableStatus : StatusEffect
{
    public InvisableStatus(float _duration) : base(_duration) { }

    public override void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }
}