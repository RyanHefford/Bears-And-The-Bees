using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    private Image popupBlock;
    private Image itemIcon;
    private Text itemDescription;

    private Sprite[] iconList;

    private string[] descriptionList = { 
        "New Vans!!\n\nJump Height Way Up!\nMove Speed Slightly Up!",
        "Fluorescent Belt!!\n\nMove Speed Way Up!!\nVisibility Up!"
    };

    private bool fadeIn = false;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeItem(int id)
    {
        fadeIn = true;

        itemIcon.sprite = iconList[id];
        itemDescription.text = descriptionList[id];
        
        StartCoroutine(FadeDelay());

    }


    private IEnumerator FadeDelay()
    {
        popupBlock.CrossFadeAlpha(1, 1, false);
        itemIcon.CrossFadeAlpha(1, 1, false);
        itemDescription.CrossFadeAlpha(1, 1, false);

        yield return new WaitForSeconds(3f);

        popupBlock.CrossFadeAlpha(0, 1, false);
        itemIcon.CrossFadeAlpha(0, 1, false);
        itemDescription.CrossFadeAlpha(0, 1, false);
    }
}
