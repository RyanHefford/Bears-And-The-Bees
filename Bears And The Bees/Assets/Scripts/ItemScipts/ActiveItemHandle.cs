using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ItemList;

public class ActiveItemHandle : MonoBehaviour
{
    public ITEM currItem = 0;
    public GameObject[] activeCollection;
    private PlayerHealth playerHealth;
    private Sprite[] iconList;
    private StatusEffectHandler playerStatusHandle;
    private Image activeSprite;
    // Start is called before the first frame update
    void Start()
    {
        playerStatusHandle = GetComponent<StatusEffectHandler>();
        playerHealth = GetComponent<PlayerHealth>();
        activeSprite = GameObject.FindGameObjectWithTag("ActiveItem").GetComponent<Image>();
        //activeSprite.canvasRenderer.SetAlpha(0);
        iconList = Resources.LoadAll<Sprite>("ItemImages/Items");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxisRaw("ActivateItem") == 1 && currItem != ITEM.VANS)
        {
            ActivateCurrentItem();
        }
    }

    private void ActivateCurrentItem()
    {
        switch (currItem)
        {
            case ITEM.SMOKE_BOMB:
                SpeedStatus speedStatus = ScriptableObject.CreateInstance<SpeedStatus>();
                speedStatus.Init(2f, 0.15f);
                playerStatusHandle.AddStatus(speedStatus);

                InvisibleStatus invisibleStatus = ScriptableObject.CreateInstance<InvisibleStatus>();
                invisibleStatus.Init(5f);
                playerStatusHandle.AddStatus(invisibleStatus);
                ResetItem();
                break;
            case ITEM.ROSE:
                SpeedStatus s = ScriptableObject.CreateInstance<SpeedStatus>();
                s.Init(12f, 0.15f);
                playerStatusHandle.AddStatus(s);
                SlowStatus b = ScriptableObject.CreateInstance<SlowStatus>();
                b.Init(10f, 0.45f);
                playerStatusHandle.AddStatus(b);
                InvisibleStatus r = ScriptableObject.CreateInstance<InvisibleStatus>();
                r.Init(6f);
                playerStatusHandle.AddStatus(r);
                ResetItem();
                break;
            case ITEM.LUNCH_BOX:
                playerHealth.Heal(6);
                SlowStatus slowStatus = ScriptableObject.CreateInstance<SlowStatus>();
                slowStatus.Init(5f, 0.4f);
                playerStatusHandle.AddStatus(slowStatus);
                ResetItem();
                break;
        }
    }

    private void ResetItem()
    {
        currItem = ITEM.VANS;
        activeSprite.canvasRenderer.SetAlpha(0);
    }

    public void NewActive(ITEM itemId)
    {
        currItem = itemId;
        activeSprite.sprite = iconList[(uint)itemId];
        activeSprite.canvasRenderer.SetAlpha(255f);
    }

    public void SwapActive(Transform oldItemLocation)
    {
        //check if slot is already empty
        if (currItem != ITEM.VANS)
        {
            foreach (GameObject currentItemCheck in activeCollection)
            {
                if (currentItemCheck.GetComponent<ItemScript>().ID == currItem)
                {
                    GameObject tempObject = Instantiate<GameObject>(currentItemCheck);
                    tempObject.transform.position = oldItemLocation.position;
                    break;
                }
            }
        }
    }
}
