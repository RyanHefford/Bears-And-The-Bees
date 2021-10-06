using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemList;

public class ItemHandle : MonoBehaviour
{
    PlayerMovement playerMove;
    PlayerHealth playerHealth;
    ActiveItemHandle activeHandle;
    public bool hasActiveItem = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        activeHandle = GetComponent<ActiveItemHandle>();
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
                playerMove.baseStats.jumpSpeed += 3f;
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
        }

        FindObjectOfType<Canvas>().GetComponentInChildren<ItemPopup>().ChangeItem(item);
    }
}
