using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandle : MonoBehaviour
{
    PlayerMovement playerMove;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupItem(int ID)
    {
        switch (ID)
        {
            //shoes
            case 0:
                playerMove.playerSpeed += 1f;
                playerMove.jumpSpeed += 3f;
                break;
            //belt
            case 1:
                playerMove.playerSpeed += 3f;
                playerMove.visibility += 1f;
                break;
            //chicken
            case 2:
                playerMove.playerSpeed -= 1.5f;
                playerHealth.ChangeMaxHealth(2);
                break;
        }

        FindObjectOfType<Canvas>().GetComponentInChildren<ItemPopup>().ChangeItem(ID);
    }
}
