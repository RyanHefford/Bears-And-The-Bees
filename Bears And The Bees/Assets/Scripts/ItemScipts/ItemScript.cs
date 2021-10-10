using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static ItemList;

public class ItemScript : MonoBehaviour
{
    public ITEM ID = 0;
    public AudioSource pickup;
    private float timeInitalized;
    private float tradeDelay = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        timeInitalized = Time.time;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Time.time >= timeInitalized + tradeDelay)
        {
            ItemHandle itemHandle = other.gameObject.GetComponent<ItemHandle>();
            itemHandle.PickupItem(ID, transform);

            Destroy(this.gameObject);
        }
    }
}
