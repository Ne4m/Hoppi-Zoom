using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Linq;
using System.Reflection;

public class SkinManager : MonoBehaviour
{

    public static SkinManager instance;

    [SerializeField] private TMP_Text currencyTxt;
    [SerializeField] private Button backBtn;
    [SerializeField] private bool isUI;

    [SerializeField] private TMP_Text titleText;

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

    private Sprite[] characterAllHeadAccessories;
    private Sprite[] characterAllBodyAccessories;

    [Header("BOTTOM TAB CONFIGURATIONS")]
    [SerializeField] private float tabClickedOffset;
    [SerializeField] private GameObject tabParent;
    [SerializeField] private List<GameObject> tabBases = new List<GameObject>();
    [SerializeField] private List<Button> tabButtons = new List<Button>();
    [SerializeField] private Vector3[] tabStartPositions;

    [SerializeField] private Sprite btnClicked_Sprite;
    [SerializeField] private Sprite btnUnclicked_Sprite;

    private UnityAction[] buttonActions;

    [Header("SKIN AREA CONFIGURATIONS")]
    [SerializeField] private GameObject skinAreaPanel;
    [SerializeField] private GameObject accessoryContainer_Prefab;
    private List<GameObject> lastInstantiatedSkins = new List<GameObject>();

   


    private enum HAT_SKINS
    {
        skin_50sMilitary = 128,
        skin_50sNurse = 273,
        skin_antlers = 132,
        skin_army = 44,
        skin_baseballCap = 511,
        skin_beanie = 1262,
        skin_beanieWithTassels = 571,
        skin_birthday = 800
    }

    private enum HAIR_SKINS
    {
        skin_shark = 100,
        skin_spartan = 300,
        skin_bullHorns = 150
    }

    private enum BODY_SKINS
    {
        skin_skeleton = 31,
        skin_beret = 500,
        skin_captains = 300
    }


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        InitializeBottomContainer();
        RefreshUI();
    }

    private void Update()
    {
        if(isUI) currencyTxt.text = SPrefs.GetInt("gameCurrency", 0).ToString();

        RefreshUI();

    }

    private void RefreshTabContent(Type type)
    {
        if(lastInstantiatedSkins != null && lastInstantiatedSkins.Count > 0)
        {
            for(int i=0; i < lastInstantiatedSkins.Count; i++)
            {
                Destroy(lastInstantiatedSkins[i]);
            }

            lastInstantiatedSkins.Clear();
        }

        foreach (FieldInfo item in type.GetFields())
            if (item.IsStatic)
            {
                // print($"Name {item.Name} Value: {(int) item.GetValue(item)}");


                var newItem = Instantiate(accessoryContainer_Prefab, skinAreaPanel.transform);

                lastInstantiatedSkins.Add(newItem);

                var skinPrice = (int)item.GetValue(item);
                var skinName = (item.Name);


                // newItem.transform.GetChild(1).GetComponent<TMP_Text>().text = skinPrice.ToString();

                for (int i = 0; i < newItem.transform.childCount; i++)
                {
                    if (newItem.transform.GetChild(i).transform.name == "PriceText")
                    {
                        var image = newItem.transform.GetChild(i).GetComponent<TMP_Text>();
                        image.text = skinPrice.ToString();
                    }
                    else if (newItem.transform.GetChild(i).transform.name == "SkinImage")
                    {
                        // Resources.LoadAll<Sprite>("Accessories/Hats")

                        skinName = skinName.Remove(0, 5);

                        var skinImage = newItem.transform.GetChild(i).GetComponent<Image>();
                        skinImage.sprite = Resources.Load<Sprite>($"Accessories/Hats/{skinName}");
                    }
                }

            }
    }

    private void InitializeBottomContainer()
    {

        for (int i = 0; i < tabParent.transform.childCount; i++)
        {
            tabBases.Add(tabParent.transform.GetChild(i).gameObject);
            tabButtons.Add(tabBases[i].transform.GetChild(0).gameObject.GetComponent<Button>());
        }

        // Array Initializations
        buttonActions = new UnityAction[tabButtons.Count];
        tabStartPositions = new Vector3[tabBases.Count];


        buttonActions[0] = Tab1_Button_Clicked;
        buttonActions[1] = Tab2_Button_Clicked;
        buttonActions[2] = Tab3_Button_Clicked;

        for (int i = 0; i < tabButtons.Count; i++)
        {
            if (tabButtons[i] != null) tabButtons[i].onClick.AddListener(buttonActions[i]);
        }


        for (int i = 0; i < tabBases.Count; i++)
        {
            tabStartPositions[i] = tabBases[i].transform.localPosition;
        }
    }

    private void Tab1_Button_Clicked()
    {
        Debug.Log("Button 1 Clicked");


        RefreshTabContent(typeof(HAT_SKINS));

        SkinMenu_TabBtnClicked(0);

    }

    private void Tab2_Button_Clicked()
    {
        Debug.Log("Button 2 Clicked");

        RefreshTabContent(typeof(HAIR_SKINS));

        SkinMenu_TabBtnClicked(1);
    }

    private void Tab3_Button_Clicked()
    {
        Debug.Log("Button 3 Clicked");

        RefreshTabContent(typeof(BODY_SKINS));

        SkinMenu_TabBtnClicked(2);

    }

    public void SkinMenu_TabBtnClicked(int index)
    {

        for(int i=0; i<tabBases.Count; i++)
        {
            tabBases[i].transform.localPosition = tabStartPositions[i];
        }

        for(int i=0; i< tabButtons.Count; i++)
        {
            tabButtons[i].image.sprite = btnUnclicked_Sprite;
        }


        var newPos = tabBases[index].transform.localPosition;
        newPos.y += tabClickedOffset;
        tabBases[index].transform.localPosition = newPos;

        tabButtons[index].image.sprite = btnClicked_Sprite;

    }

    public void RefreshUI()
    {
        if (isUI)
        {
            characterSkins = Resources.LoadAll<Sprite>("Characters");
            selectedCharacter.GetComponent<Image>().sprite = characterSkins[SPrefs.GetInt("LastSelectedCharacterIndex", 0)];

            characterHatImg = characterHat.GetComponent<Image>();
            characterBodyImg = characterBody.GetComponent<Image>();

            characterAllHeadAccessories = Resources.LoadAll<Sprite>("Accessories/Hats");
            characterAllBodyAccessories = Resources.LoadAll<Sprite>("Accessories/Body");

            for (int i = 0; i < hatContainer.transform.childCount-1; i++)
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

            sr.sprite = characterSkins[SPrefs.GetInt("LastSelectedCharacterIndex", 0)];
        }

        EquipHeadAccessory(SPrefs.GetString("PlayerHat", "none"));
        EquipBodyAccessory(SPrefs.GetString("PlayerBody", "none"));

    }

    public void UnequipHeadAccessory()
    {
        SPrefs.SetString("PlayerHat", "none");
        EquipHeadAccessory(SPrefs.GetString("PlayerHat", "none"));
    }

    public void UnequipBodyAccessory()
    {
        SPrefs.SetString("PlayerBody", "none");
        EquipBodyAccessory(SPrefs.GetString("PlayerBody", "none"));
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

    public void SetTitleText(string txt)
    {
        titleText.text = txt;
    }

    private int GetHeadAccessoriesCount()
    {
        return 10;
    }

    private int GetHairsCount()
    {
        return 5;
    }

    private int GetBodyAccessoriesCount()
    {
        return 15;
    }



}
