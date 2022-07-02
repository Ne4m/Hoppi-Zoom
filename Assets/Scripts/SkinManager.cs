using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private bool isUI;

    private SpriteRenderer sr;

    [Header("Player Skins")]
    [SerializeField] private Sprite[] characterSkins;

    [Header("Player Accessories")]
    [SerializeField] private GameObject characterHat;
    [SerializeField] private GameObject characterEye;
    private SpriteRenderer characterHatSR;
    private SpriteRenderer characterEyeSR;

    private Image characterHatImg;
    private Image characterEyeImg;


    void Start()
    {
        if (isUI)
        {
            characterHatImg = characterHat.GetComponent<Image>();
            characterEyeImg = characterEye.GetComponent<Image>();

        }
        else
        {
            sr = GetComponent<SpriteRenderer>();

            characterHatSR = characterHat.GetComponent<SpriteRenderer>();
            characterEyeSR = characterEye.GetComponent<SpriteRenderer>();

            characterSkins = Resources.LoadAll<Sprite>("Characters");

            sr.sprite = characterSkins[PlayerPrefs.GetInt("LastSelectedCharacterIndex", 0)];
        }

        EquipHeadAccessory("crown");
        EquipEyeAccessory("none");

    }


    private void EquipHeadAccessory(string itemName)
    {
        if(itemName == "none")
        {
            if (!isUI) characterHatSR.sprite = null;
            else characterHatImg.color = new Color32(255, 255, 255, 0);
            return;
        }

        if(!isUI) characterHatSR.sprite = Resources.Load<Sprite>($"Accessories/Hats/{itemName}");
        else
        {
            characterHatImg.color = new Color32(255, 255, 255, 255);
            characterHatImg.sprite = Resources.Load<Sprite>($"Accessories/Hats/{itemName}");
            characterHat.GetComponent<RectTransform>().pivot = characterHatImg.sprite.pivot / characterHatImg.sprite.rect.size;
        }
            

    }

    private void EquipEyeAccessory(string itemName)
    {
        if (itemName == "none")
        {
            if (!isUI) characterEyeSR.sprite = null;
            else characterEyeImg.color = new Color32(255, 255, 255, 0);
            return;
        }

        if (!isUI) characterEyeSR.sprite = Resources.Load<Sprite>($"Accessories/Eyes/{itemName}");
        else
        {
            characterEyeImg.color = new Color32(255, 255, 255, 255);
            characterEyeImg.sprite = Resources.Load<Sprite>($"Accessories/Eyes/{itemName}");
            characterEye.GetComponent<RectTransform>().pivot = characterEyeImg.sprite.pivot / characterEyeImg.sprite.rect.size;
        }
            
    }

}
