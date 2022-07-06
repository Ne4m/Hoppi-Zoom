using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyTxt;
    [SerializeField] private Button backBtn;
    [SerializeField] private bool isUI;


    private SpriteRenderer sr;

    [Header("Player Skins")]
    [SerializeField] private Sprite[] characterSkins;

    [Header("Player Accessories")]
    [SerializeField] private GameObject selectedCharacter;
    [SerializeField] private GameObject characterHat;
    [SerializeField] private GameObject characterBody;

  

    private SpriteRenderer characterHatSR;
    private SpriteRenderer characterBodySR;

    public Image characterHatImg;
    public Image characterBodyImg;


    [Header("Player Accessory Containers")]
    [SerializeField] private GameObject hatContainer;
    [SerializeField] private GameObject bodyContainer;


    public Sprite[] characterAllHeadAccessories;
    public Sprite[] characterAllBodyAccessories;
    void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        if(isUI) currencyTxt.text = PlayerPrefs.GetInt("gameCurrency", 0).ToString();

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (isUI)
        {
            characterSkins = Resources.LoadAll<Sprite>("Characters");
            selectedCharacter.GetComponent<Image>().sprite = characterSkins[PlayerPrefs.GetInt("LastSelectedCharacterIndex", 0)];

            characterHatImg = characterHat.GetComponent<Image>();
            characterBodyImg = characterBody.GetComponent<Image>();

            characterAllHeadAccessories = Resources.LoadAll<Sprite>("Accessories/Hats");
            characterAllBodyAccessories = Resources.LoadAll<Sprite>("Accessories/Body");

            for (int i = 0; i < hatContainer.transform.childCount; i++)
            {
                hatContainer.transform.GetChild(i).GetComponent<Image>().sprite = characterAllHeadAccessories[i];
            }

            for (int i = 0; i < characterAllBodyAccessories.Length; i++) // bodyContainer.transform.childCount
            {
                bodyContainer.transform.GetChild(i).GetComponent<Image>().sprite = characterAllBodyAccessories[i];
            }

            if (backBtn != null) backBtn.onClick.AddListener(backBtnClicked);
        }
        else
        {
            sr = GetComponent<SpriteRenderer>();

            characterHatSR = characterHat.GetComponent<SpriteRenderer>();
            characterBodySR = characterBody.GetComponent<SpriteRenderer>();

            characterSkins = Resources.LoadAll<Sprite>("Characters");

            sr.sprite = characterSkins[PlayerPrefs.GetInt("LastSelectedCharacterIndex", 0)];
        }

        EquipHeadAccessory(PlayerPrefs.GetString("PlayerHat", "none"));
        EquipBodyAccessory(PlayerPrefs.GetString("PlayerBody", "none"));

    }

    private void backBtnClicked()
    {

        MainMenu_Controller mainMenu;
        mainMenu = MainMenu_Controller.instance;
        mainMenu.canvasBackMainMenu();
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
        else // if UI
        {
            characterHatImg.color = new Color32(255, 255, 255, 255);
            characterHatImg.sprite = Resources.Load<Sprite>($"Accessories/Hats/{itemName}");
            characterHat.GetComponent<RectTransform>().pivot = characterHatImg.sprite.pivot / characterHatImg.sprite.rect.size;


        }
            

    }

    private void EquipBodyAccessory(string itemName)
    {
        if (itemName == "none")
        {
            if (!isUI) characterBodySR.sprite = null;
            else characterBodyImg.color = new Color32(255, 255, 255, 0);
            return;
        }

        if (!isUI) characterBodySR.sprite = Resources.Load<Sprite>($"Accessories/Body/{itemName}");
        else
        {
            characterBodyImg.color = new Color32(255, 255, 255, 255);
            characterBodyImg.sprite = Resources.Load<Sprite>($"Accessories/Body/{itemName}");
            characterBody.GetComponent<RectTransform>().pivot = characterBodyImg.sprite.pivot / characterBodyImg.sprite.rect.size;
        }
            
    }



}
