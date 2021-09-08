using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandle : MonoBehaviour
{
    PlayerMovement playerMove;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
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
                break;
        }

        FindObjectOfType<Canvas>().GetComponentInChildren<ItemPopup>().ChangeItem(ID);
    }
}
