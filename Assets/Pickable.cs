using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private GameObject insideObject;
    private SpriteRenderer insideObjectSprite;

    private GiftSpawner.Pickables chosenPickable;
    void Start()
    {
        insideObject = transform.GetChild(0).gameObject;
        insideObjectSprite = insideObject.GetComponent<SpriteRenderer>();

        switch (GiftSpawner.instance.ChosenPickable)
        {
            case GiftSpawner.Pickables.GOLD:

                insideObjectSprite.sprite = Resources.Load<Sprite>("Pickables/Gold");
                chosenPickable = GiftSpawner.Pickables.GOLD;
                break;

            case GiftSpawner.Pickables.HEALTH:
                insideObjectSprite.sprite = Resources.Load<Sprite>("Pickables/Health");
                chosenPickable = GiftSpawner.Pickables.HEALTH;
                break;

            case GiftSpawner.Pickables.AMMO:

                insideObjectSprite.sprite = Resources.Load<Sprite>("Pickables/Ammo");
                chosenPickable = GiftSpawner.Pickables.AMMO; ;
                break;
        }

        //Debug.Log($"Log from Instantiated object: {GiftSpawner.instance.ChosenPickable}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);

            switch (chosenPickable)
            {
                case GiftSpawner.Pickables.GOLD:
                    Debug.Log("Gained Gold Through Pickable!");

                    LevelManager.instance.AddCurrentCurrency(Random.Range(1, 10));
                    break;

                case GiftSpawner.Pickables.HEALTH:
                    Debug.Log("Gained Health Through Pickable!");

                    PlayerConditions.instance.HealthbarShow = true;
                    LevelManager.instance.playerControl.percentageHeal(10);
                    break;

                case GiftSpawner.Pickables.AMMO:

                    Debug.Log("Gained Ammo Through Pickable!");
                    Shooting.instance.AddAmmo(Random.Range(1, 3));
                    break;
            }
        }
    }
}
