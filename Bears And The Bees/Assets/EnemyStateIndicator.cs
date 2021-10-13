using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateIndicator : MonoBehaviour
{
    public Sprite alertIcon;
    public Sprite searchingIcon;
    public Sprite stunnedIcon;
    public Sprite chasingIcon;
    private Transform cameraTransform;
    public EnemyVision vision;
    private Image image;
    public Image coverImage;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraTransform);

        switch (vision.getState()) {
            case EnemyVision.STATE.PASSIVE:
                image.enabled = false;
                break;
            case EnemyVision.STATE.ALERT:
                if (!image.enabled)
                {
                    image.enabled = true;
                }

                if (!coverImage.enabled)
                {
                    coverImage.enabled = true;
                }
                image.sprite = alertIcon;
                break;
            case EnemyVision.STATE.CHASING:
                if (!image.enabled)
                {
                    image.enabled = true;
                }

                if (coverImage.enabled)
                {
                    coverImage.enabled = false;
                }

                image.sprite = chasingIcon;
                break;
            case EnemyVision.STATE.SEARCHING:
                if (!image.enabled)
                {
                    image.enabled = true;
                }
                if (!coverImage.enabled)
                {
                    coverImage.enabled = true;
                }

                image.sprite = alertIcon;
                break;
            case EnemyVision.STATE.STUNNED:
                if (!image.enabled)
                {
                    image.enabled = true;
                }
                if (coverImage.enabled)
                {
                    coverImage.enabled = false;
                }

                image.sprite = stunnedIcon;
                break;

        }
        coverImage.fillAmount = vision.percentageAlert;

    }

    public void IsAlert()
    {
        if (!image.enabled)
        {
            image.enabled = true;
            image.sprite = alertIcon;
        }
    }

    public void IsChasing()
    {
        if (!image.enabled)
        {
            image.enabled = true;
            image.sprite = chasingIcon;
        }
    }
}
