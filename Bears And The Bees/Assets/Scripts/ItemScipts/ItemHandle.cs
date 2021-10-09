using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemList;

public class ItemHandle : MonoBehaviour
{
    private PlayerMovement playerMove;
    private PlayerHealth playerHealth;
    private ActiveItemHandle activeHandle;
    private StatusEffectHandler playerStatusHandle;
    public bool hasActiveItem = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        activeHandle = GetComponent<ActiveItemHandle>();
        playerStatusHandle = GetComponent<StatusEffectHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupItem(ITEM item, Transform transform)
    {
        switch (item)
        {
            //vans
            case ITEM.VANS:
                playerMove.baseStats.moveSpeed += 1f;
                playerMove.baseStats.jumpSpeed += 1.5f;
                break;
            //belt
            case ITEM.BELT:
                playerMove.baseStats.moveSpeed += 3f;
                playerMove.baseStats.visibility += 1f;
                break;
            //chicken
            case ITEM.CHICKEN:
                playerMove.baseStats.moveSpeed -= 1.5f;
                playerHealth.ChangeMaxHealth(2);
                break;
            //smoke bomb
            case ITEM.SMOKE_BOMB:
                activeHandle.SwapActive(transform);
                activeHandle.NewActive(ITEM.SMOKE_BOMB);
                break;
            //rose
            case ITEM.ROSE:
                activeHandle.SwapActive(transform);
                activeHandle.NewActive(ITEM.ROSE);
                break;
            //lunch box
            case ITEM.LUNCH_BOX:
                activeHandle.SwapActive(transform);
                activeHandle.NewActive(ITEM.LUNCH_BOX);
                break;
            //coffee
            case ITEM.COFFEE:
                activeHandle.SwapActive(transform);
                activeHandle.NewActive(ITEM.COFFEE);
                break;
            //gummy bear
            case ITEM.GUMMY_BEAR:
                playerMove.baseStats.jumpSpeed += 4f;
                playerMove.baseStats.visibility += 1f;
                break;
            //coke
            case ITEM.COKE:
                playerMove.baseStats.jumpSpeed += 1f;
                playerMove.baseStats.visibility += 1.5f;
                playerMove.baseStats.moveSpeed += 1.5f;
                playerHealth.ChangeMaxHealth(2);
                break;
            //statue
            case ITEM.STATUE:
                activeHandle.SwapActive(transform);
                activeHandle.NewActive(ITEM.STATUE);
                break;
            //high heels
            case ITEM.HIGH_HEELS:
                playerHealth.ChangeMaxHealth(4);
                StealthDownStatus stealthDown = ScriptableObject.CreateInstance<StealthDownStatus>();
                stealthDown.Init(20f, 0.3f);
                playerStatusHandle.AddStatus(stealthDown);
                NoiseUpStatus noiseUp = ScriptableObject.CreateInstance<NoiseUpStatus>();
                noiseUp.Init(20f, 0.3f);
                playerStatusHandle.AddStatus(noiseUp);
                break;
                
        }

        FindObjectOfType<Canvas>().GetComponentInChildren<ItemPopup>().ChangeItem(item);
    }
}
