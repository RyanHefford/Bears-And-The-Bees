using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScript : MonoBehaviour
{
    private StatusEffectIcons iconHandle;

    public float maxDuration;
    public float durationLeft;
    public string statusTypeName;
    private Image iconCover;
    public Image iconImage;
    // Start is called before the first frame update
    void Start()
    {
        iconHandle = GetComponentInParent<StatusEffectIcons>();

        iconImage = GetComponentsInChildren<Image>()[0];
        iconCover = GetComponentsInChildren<Image>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        durationLeft -= Time.deltaTime;
        if (iconImage.enabled && durationLeft <= 0)
        {
            iconImage.enabled = false;
            iconCover.enabled = false;
            iconHandle.RemoveStatus(iconImage);
        }
        else
        {
            iconCover.fillAmount = 1 - durationLeft / maxDuration;
        }
    }

    public void ActivateIcon(Sprite _sprite, string typeName, float _durationLeft, float _maxDuration = 0)
    {
        iconImage.enabled = true;
        iconCover.enabled = true;

        maxDuration = _maxDuration == 0 ? _durationLeft : _maxDuration;
        durationLeft = _durationLeft;

        statusTypeName = typeName;

        iconImage.sprite = _sprite;
    }

    public void CombineEffects(float _newDuration)
    {
        if (_newDuration >= maxDuration)
        {
            maxDuration = _newDuration;
            durationLeft = _newDuration;
        }
        else if(_newDuration >= durationLeft)
        {
            durationLeft = _newDuration;
            maxDuration = durationLeft;
        }
    }

    public void Empty()
    {
        iconImage.enabled = false;
        iconCover.enabled = false;

        maxDuration = 0;
        durationLeft = 0;
    }
}
