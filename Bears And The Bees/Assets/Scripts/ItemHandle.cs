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
                playerMove.playerSpeed += 2.5f;
                playerMove.jumpSpeed += 3.3f;
                break;
        }
    }
}
