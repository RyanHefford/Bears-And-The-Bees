using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using static ItemList;

public class ActiveItemHandle : MonoBehaviour
{
    private ITEM currItem = ITEM.VANS;
    public GameObject[] activeCollection;
    private PlayerHealth playerHealth;
    private Sprite[] iconList;
    private StatusEffectHandler playerStatusHandle;
    private Image activeSprite;
    public GameObject roseThrowable;
    private GameObject[] enemies;

    public AudioSource bombSound;
    public AudioSource roseSound;
    public AudioSource lunchboxSound;
    public AudioSource coffeeSound;

    // Start is called before the first frame update
    void Start()
    {
        playerStatusHandle = GetComponent<StatusEffectHandler>();
        playerHealth = GetComponent<PlayerHealth>();
        activeSprite = GameObject.FindGameObjectWithTag("ActiveItem").GetComponent<Image>();
        iconList = Resources.LoadAll<Sprite>("ItemImages/Items");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
                bombSound.Play();
                SpeedStatus speedStatus = ScriptableObject.CreateInstance<SpeedStatus>();
                speedStatus.Init(2f, 0.15f);
                playerStatusHandle.AddStatus(speedStatus);

                InvisibleStatus invisibleStatus = ScriptableObject.CreateInstance<InvisibleStatus>();
                invisibleStatus.Init(5f);
                playerStatusHandle.AddStatus(invisibleStatus);

                HandleEnemyStun(50f, 2f);

                ResetItem();
                break;
            case ITEM.ROSE:
                //roseSound.Play();
                GameObject tempRose = Instantiate<GameObject>(roseThrowable);
                tempRose.transform.position = transform.position + transform.forward;
                tempRose.GetComponent<Rigidbody>().AddForce(transform.forward * 800);
                ResetItem();
                break;
            case ITEM.LUNCH_BOX:
                lunchboxSound.Play();
                playerHealth.Heal(PlayerPrefs.GetInt("PlayerBeginningHealth") / 2);
                SlowStatus slowStatus = ScriptableObject.CreateInstance<SlowStatus>();
                slowStatus.Init(5f, 0.4f);
                playerStatusHandle.AddStatus(slowStatus);
                ResetItem();
                break;
            case ITEM.COFFEE:
                coffeeSound.Play();
                StartCoroutine(CoffeeEffect());
                ResetItem();
                break;
            case ITEM.STATUE:
                //todo
                roseSound.Play();
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

    private void HandleEnemyStun(float maxAlertDistance, float stunDuration)
    {
        if (enemies.Length == 0) { enemies = GameObject.FindGameObjectsWithTag("Enemy"); }
        foreach (GameObject enemy in enemies)
        {
            // check if enemy is within alert distance
            if (Vector3.Distance(transform.position, enemy.transform.position) <= maxAlertDistance)
            {
                if (enemy.GetComponentInChildren<EnemyVision>() != null)
                {
                    enemy.GetComponentInChildren<EnemyVision>().StunEnemy(stunDuration);
                }
            }
        }
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

    private IEnumerator CoffeeEffect()
    {
        float coffeeDuration = 7f;

        SpeedStatus speedUp = ScriptableObject.CreateInstance<SpeedStatus>();
        speedUp.Init(coffeeDuration, 0.4f);
        StealthUpStatus stealthUp = ScriptableObject.CreateInstance<StealthUpStatus>();
        stealthUp.Init(coffeeDuration, 0.4f);
        NoiseDownStatus noiseDown = ScriptableObject.CreateInstance<NoiseDownStatus>();
        noiseDown.Init(coffeeDuration, 0.4f);
        JumpUpStatus jumpUp = ScriptableObject.CreateInstance<JumpUpStatus>();
        jumpUp.Init(coffeeDuration, 0.3f);


        playerStatusHandle.AddStatus(speedUp);
        playerStatusHandle.AddStatus(stealthUp);
        playerStatusHandle.AddStatus(noiseDown);
        playerStatusHandle.AddStatus(jumpUp);


        yield return new WaitForSeconds(coffeeDuration);

        SlowStatus speedDown = ScriptableObject.CreateInstance<SlowStatus>();
        speedDown.Init(coffeeDuration, 0.4f);
        StealthDownStatus stealthDown = ScriptableObject.CreateInstance<StealthDownStatus>();
        stealthDown.Init(coffeeDuration, 0.4f);
        NoiseUpStatus noiseUp = ScriptableObject.CreateInstance<NoiseUpStatus>();
        noiseUp.Init(coffeeDuration, 0.4f);
        JumpDownStatus jumpDown = ScriptableObject.CreateInstance<JumpDownStatus>();
        jumpDown.Init(coffeeDuration, 0.3f);


        playerStatusHandle.AddStatus(speedDown);
        playerStatusHandle.AddStatus(stealthDown);
        playerStatusHandle.AddStatus(noiseUp);
        playerStatusHandle.AddStatus(jumpDown);
    }
}
