using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectIcons : MonoBehaviour
{
    private IconScript[] slots;
    private Sprite slowSprite;
    private Sprite speedSprite;
    private Sprite invisibleSprite;
    private Sprite jumpUpSprite;
    private Sprite jumpDownSprite;
    private Sprite stealthUpSprite;
    private Sprite stealthDownSprite;
    private Sprite noiseUpSprite;
    private Sprite noiseDownSprite;
    private int numSlotsActive = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] listOfSlots = GameObject.FindGameObjectsWithTag("StatusIcon");

        slots = new IconScript[listOfSlots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = listOfSlots[i].GetComponent<IconScript>();
        }

        slowSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/SpeedDown");
        speedSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/SpeedUp");
        invisibleSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/Invisible");
        jumpUpSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/JumpUp");
        jumpDownSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/JumpDown");
        stealthUpSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/StealthUp");
        stealthDownSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/StealthDown");
        noiseUpSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/NoiseUp");
        noiseDownSprite = Resources.Load<Sprite>("ItemImages/StatusEffects/NoiseDown");
    }

    internal void RemoveStatus(Image iconImage)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].iconImage == iconImage)
            {
                if (i != 0 && slots[i-1] == null)
                {
                    slots[i - 1].ActivateIcon(slots[i].iconImage.sprite, slots[i].GetType().Name, slots[i].durationLeft, slots[i].maxDuration);
                    slots[i].Empty();
                }
            }
        }
        numSlotsActive--;
    }

    public void AddStatusIcon(StatusEffect effect)
    {
        switch (effect.GetType().Name)
            {
            case "SlowStatus":
                slots[numSlotsActive].ActivateIcon(slowSprite, effect.GetType().Name, effect.durationLeft);
                numSlotsActive++;
                break;
            case "SpeedStatus":
                slots[numSlotsActive].ActivateIcon(speedSprite, effect.GetType().Name, effect.durationLeft);
                numSlotsActive++;
                break;
            case "InvisibleStatus":
                slots[numSlotsActive].ActivateIcon(invisibleSprite, effect.GetType().Name, effect.durationLeft);
                numSlotsActive++;
                break;
        }
    }

    public void UpdateExisting(StatusEffect effect)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].statusTypeName == effect.GetType().Name)
            {
                slots[i].CombineEffects(effect.durationLeft);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
