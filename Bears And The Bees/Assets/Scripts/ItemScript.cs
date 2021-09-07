using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public int ID = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Here");
        if (other.gameObject.CompareTag("Player"))
        {
            ItemHandle itemHandle = other.gameObject.GetComponent<ItemHandle>();
            itemHandle.PickupItem(ID);


            Destroy(this.gameObject);
        }
    }
}