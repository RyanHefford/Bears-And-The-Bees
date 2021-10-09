using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ItemList;

public class ItemPopup : MonoBehaviour
{
    private Image popupBlock;
    private Image itemIcon;
    private Text itemDescription;

    private Sprite[] iconList;


    // Start is called before the first frame update
    void Start()
    {
        popupBlock = GetComponent<Image>();
        itemIcon = GameObject.Find("ItemImage").GetComponent<Image>();
        itemDescription = GetComponentInChildren<Text>();

        iconList = Resources.LoadAll<Sprite>("ItemImages/Items");

        popupBlock.canvasRenderer.SetAlpha(0);
        itemIcon.canvasRenderer.SetAlpha(0);
        itemDescription.canvasRenderer.SetAlpha(0);

    }


    public void ChangeItem(ITEM item)
    {
        itemIcon.sprite = iconList[(uint)item];
        itemDescription.text = ItemList.GetDescription(item);
        
        StartCoroutine(FadeDelay());

    }


    private IEnumerator FadeDelay()
    {
        popupBlock.CrossFadeAlpha(1, 1, false);
        itemIcon.CrossFadeAlpha(1, 1, false);
        itemDescription.CrossFadeAlpha(1, 1, false);

        yield return new WaitForSeconds(5f);

        popupBlock.CrossFadeAlpha(0, 1, false);
        itemIcon.CrossFadeAlpha(0, 1, false);
        itemDescription.CrossFadeAlpha(0, 1, false);
    }
}
