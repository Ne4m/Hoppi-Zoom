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
    private string skinPath;
    private string lastClickedSkinName;

    [Header("SKIN UNLOCK AREA")]
    [SerializeField] private GameObject unlockSkin_Panel;
    [SerializeField] private Button unlockButton;
    [SerializeField] private Button adUnlockButton;
    [SerializeField] private Image skinImage;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text skinTitle;

    private Coroutine DistributeAdReward;
    private bool isAdRewardTaskRunning = false;

    private int discountedPrice;
    private int lastSkinPrice;
    private string lastSkinName;

    private Sprite lastClickedSkinSprite;
    private Color32 NONE_COLOR = new Color32(255, 255, 255, 0);


    [SerializeField] private UIMessager messager;

    private enum HAT_SKINS
    {
        skin_purple_hat = 128,
        skin_50sMilitary = 100,
        skin_50sNurse = 200,
        skin_antlers = 300,
        skin_army = 400,
        skin_baseballCap = 500,
        skin_beanie = 234,
        skin_beanieWithTassels = 634,
        skin_beret = 111,
        skin_bicorn = 1234,
        skin_birthday = 2340,
        skin_bowlerHat = 111,
        skin_bullHorns = 522,
        skin_captains = 500,
        skin_classicFedora = 220,
        skin_cowboy = 234,
        skin_cowboy_hat = 51

    }

    private enum HAIR_SKINS
    {
        skin_hair_1 = 100,
        skin_hair_2 = 200,
        skin_hair_3 = 1300

    }

    private enum BODY_SKINS
    {
        skin_torso = 131,
        skin_torso_blue = 1345,
        skin_torso_black = 222,
        skin_sun_glasses = 150

    }


    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        //lockEveryItems();

        messager = gameObject.GetComponent<UIMessager>();

        if (backBtn != null) backBtn.onClick.AddListener(BackBtn_Clicked);
        if (unlockButton != null) unlockButton.onClick.AddListener(UnlockButton_Clicked);
        if (adUnlockButton != null) adUnlockButton.onClick.AddListener(AdUnlockButton_Clicked);

        if (isUI) InitializeBottomContainer();

        // lockEveryItems();
        //SPrefs.DeleteKey("PlayerHead");
        //SPrefs.DeleteKey("PlayerBody");





        //UnequipBodyAccessory();
        //UnequipHeadAccessory();

        skinPath = SPrefs.GetString("LastSkinPath", "Hats");

        RefreshUI();
        var head = SPrefs.GetString("PlayerHead", "none");
        var body = SPrefs.GetString("PlayerBody", "none");

        Debug.Log($"Start Head : {head} - Body : {body}");
        EquipHeadAccessory(head);
        EquipBodyAccessory(body);
    }

    private void Update()
    {
        if (isUI)
        {
            currencyTxt.text = SPrefs.GetInt("gameCurrency", 0).ToString();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (unlockSkin_Panel.transform.gameObject.activeSelf)
                {
                    unlockSkin_Panel.transform.gameObject.SetActive(false);
                }
                else
                {
                    transform.gameObject.SetActive(false);
                    BackBtn_Clicked();
                }


            }
        }





       // RefreshUI();

    }

    void lockEveryItems()
    {
        foreach (FieldInfo item in typeof(HAT_SKINS).GetFields())
            if (item.IsStatic)
            {

                SPrefs.SetBool($"{item.Name}_locked", true);
                SPrefs.Save();
                Debug.Log($"{item.Name} lock: {IsLocked(item.Name)}");
            }

        foreach (FieldInfo item in typeof(HAIR_SKINS).GetFields())
            if (item.IsStatic)
            {
                SPrefs.SetBool($"{item.Name}_locked", true);
                SPrefs.Save();
                Debug.Log($"{item.Name} lock: {IsLocked(item.Name)}");
            }

        foreach (FieldInfo item in typeof(BODY_SKINS).GetFields())
            if (item.IsStatic)
            {

                SPrefs.SetBool($"{item.Name}_locked", true);
                SPrefs.Save();
                Debug.Log($"{item.Name} lock: {IsLocked(item.Name)}");
            }

    }

    void UnlockEveryItems()
    {
        foreach (FieldInfo item in typeof(HAT_SKINS).GetFields())
            if (item.IsStatic)
            {

                SPrefs.SetBool($"{item.Name}_locked", false);
                Debug.Log($"{item.Name} locked!");
            }

        foreach (FieldInfo item in typeof(HAIR_SKINS).GetFields())
            if (item.IsStatic)
            {
                SPrefs.SetBool($"{item.Name}_locked", false);
                Debug.Log($"{item.Name} locked!");
            }

        foreach (FieldInfo item in typeof(BODY_SKINS).GetFields())
            if (item.IsStatic)
            {

                SPrefs.SetBool($"{item.Name}_locked", false);
                Debug.Log($"{item.Name} locked!");
            }
    }

    private void RefreshTabContent(Type type, int index)
    {
        switch (index)
        {
            case 0:
                skinPath = "Hats";
                SPrefs.SetString("LastSkinPath", skinPath);
                break;
            case 1:
                skinPath = "Hairs";
                SPrefs.SetString("LastSkinPath", skinPath);
                break;
            case 2:
                skinPath = "Body";
                break;
        }

        if (lastInstantiatedSkins != null && lastInstantiatedSkins.Count > 0)
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




                //Debug.Log($"instantiated item name : {skinName} is Locked: {IsLocked(skinName)}");

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
                        skinImage.sprite = Resources.Load<Sprite>($"Accessories/{skinPath}/{skinName}");

                        //Debug.Log($"Entrance {skinName}");


                        if (!IsLocked(skinName))
                        {
                            newItem.transform.GetChild(0).transform.gameObject.SetActive(false);
                            newItem.transform.GetChild(1).transform.gameObject.SetActive(false); // Text
                            newItem.transform.GetChild(2).transform.localPosition = new Vector3(newItem.transform.GetChild(i).transform.localPosition.x, 25, newItem.transform.GetChild(i).transform.localPosition.z);
                        }
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
        //Debug.Log("Button 1 Clicked");


        RefreshTabContent(typeof(HAT_SKINS), 0);


        SkinMenu_TabBtnClicked(0);

    }

    private void Tab2_Button_Clicked()
    {
       // Debug.Log("Button 2 Clicked");


        RefreshTabContent(typeof(HAIR_SKINS), 1);


        SkinMenu_TabBtnClicked(1);
    }

    private void Tab3_Button_Clicked()
    {
        //Debug.Log("Button 3 Clicked");


        RefreshTabContent(typeof(BODY_SKINS), 2);

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


            switch (GetSkinPath())
            {
                case "Hats":
                    Tab1_Button_Clicked();
                    break;
                case "Hairs":
                    Tab2_Button_Clicked();
                    break;
                case "Body":
                    Tab3_Button_Clicked();
                    break;
            }


        }
        else
        {
            sr = GetComponent<SpriteRenderer>();

            characterHatSR = characterHat.GetComponent<SpriteRenderer>();
            characterBodySR = characterBody.GetComponent<SpriteRenderer>();

            characterSkins = Resources.LoadAll<Sprite>("Characters");

            sr.sprite = characterSkins[SPrefs.GetInt("LastSelectedCharacterIndex", 0)];
        }



        //EquipHeadAccessory(SPrefs.GetString("PlayerHead", "none"));
        //EquipBodyAccessory(SPrefs.GetString("PlayerBody", "none"));

    }

    public void UnequipHeadAccessory()
    {
        SPrefs.SetString("PlayerHead", "none");
        EquipHeadAccessory("none");
    }

    public void UnequipBodyAccessory()
    {
        SPrefs.SetString("PlayerBody", "none");
        EquipBodyAccessory("none");
    }

    private void BackBtn_Clicked()
    {

        MainMenu_Controller mainMenu;
        mainMenu = MainMenu_Controller.instance;
        mainMenu.canvasBackMainMenu();
    }

    public string GetSkinPath()
    {
        return skinPath;
    }

    public bool IsLocked(string itemName)
    {
        return SPrefs.GetBool($"skin_{itemName}_locked", true);
    }

    public void ManageAppliedSkin(string skinName)
    {
        //Debug.Log($"Got skin {skinName} from button lock status: {IsLocked(skinName)}");

        switch (GetSkinPath())
        {
            case "Hats":

                if (!IsLocked(skinName))
                {
                    EquipHeadAccessory(skinName);
                }
                else
                {
                    RequestUnlockPage(skinName);
                }
                break;
            case "Hairs":

                if (!IsLocked(skinName))
                {
                    EquipHeadAccessory(skinName);

                }
                else
                {
                    RequestUnlockPage(skinName);
                }

                break;
            case "Body":

                if (!IsLocked(skinName))
                {
                    EquipBodyAccessory(skinName);
                }
                else
                {
                    RequestUnlockPage(skinName);

                }

                break;
        }
    }

    private void EquipHeadAccessory(string itemName)
    {


        if (itemName == "none")
        {
            if (!isUI) characterHatSR.sprite = null;

            else characterHatImg.color = NONE_COLOR;
            return;
        }

        if (!isUI)
        {
            characterHatSR.sprite = Resources.Load<Sprite>($"Accessories/{skinPath}/{itemName}");
        }
        else // if UI
        {


            characterHatImg.color = new Color32(255, 255, 255, 255);
            Debug.Log($"Skinpath before error {skinPath} and item name {itemName}");
            characterHatImg.sprite = Resources.Load<Sprite>($"Accessories/{skinPath}/{itemName}");
            characterHat.GetComponent<RectTransform>().pivot = characterHatImg.sprite.pivot / characterHatImg.sprite.rect.size;

            SPrefs.SetString("PlayerHead", itemName);
            SPrefs.Save();


        }


    }

    private void EquipBodyAccessory(string itemName)
    {
        if (itemName == "none")
        {
            if (!isUI) characterBodySR.sprite = null;
            else characterBodyImg.color = NONE_COLOR;
            return;
        }

        if (!isUI)
        {
            characterBodySR.sprite = Resources.Load<Sprite>($"Accessories/Body/{itemName}");
        }
        else // if UI
        {

            characterBodyImg.color = new Color32(255, 255, 255, 255);
            characterBodyImg.sprite = Resources.Load<Sprite>($"Accessories/Body/{itemName}");
            characterBody.GetComponent<RectTransform>().pivot = characterBodyImg.sprite.pivot / characterBodyImg.sprite.rect.size;

            SPrefs.SetString("PlayerBody", itemName);
            SPrefs.Save();


        }


    }


    public void SetLastClickedSprite(Sprite sprite)
    {
        lastClickedSkinSprite = sprite;
    }

    public Sprite GetLastClickedSprite()
    {
        return lastClickedSkinSprite;
    }

    public void RequestUnlockPage(string skinName)
    {
        // IMPLEMENT A UNLOCK PAGE HERE AND REFRESH CONTENTS AFTER UNLOCK (CONTAINS AD REWARDS)


        var price = SPrefs.GetInt("LastClickedSkinPrice", 0);
        var sprite = SPrefs.GetString("LastClickedSkin", "none");
        if(GetSkinPath() == "Hats" || GetSkinPath() == "Hairs")
        {
            ShowUnlockPanel(GetLastClickedSprite(), skinName, price);
        }
        else
        {
            ShowUnlockPanel(GetLastClickedSprite(), skinName, price);
        }


       // ManageAppliedSkin(skinName);
    }

    private void MakeTransaction(int price)
    {
        var currency = SPrefs.GetInt("gameCurrency", 0); // IMPLEMENT FROM PLAY STORE LATER;
        currency -= price;
        SPrefs.SetInt("gameCurrency", currency);
        SPrefs.Save();
    }

    private void UnlockSkin(string name)
    {
        SPrefs.SetBool($"skin_{name}_locked", false);
        SPrefs.Save();

        Debug.Log($"Unlocked {name}");
    }

    public void ShowUnlockPanel(Sprite image, string name, int price)
    {

        var currency = SPrefs.GetInt("gameCurrency", 0); // IMPLEMENT FROM PLAY STORE LATER

        if(currency >= price)
        {
            unlockSkin_Panel.transform.gameObject.SetActive(true);
            Debug.Log($"Image name: {image.name}");
            skinImage.sprite = image;
            priceText.text = price.ToString();
            skinTitle.text = name;

            discountedPrice = price / 2;

            lastSkinPrice = price;
            lastSkinName = name;
        }
        else
        {
            messager.startMsgv2("UNSUFFICIENT FUNDS!", 2f, Vector3.zero, Color.red);
            unlockSkin_Panel.transform.gameObject.SetActive(false);
        }


    }

    private void UnlockButton_Clicked()
    {
        Debug.Log($"Unlock request for {lastSkinName} received!");

        unlockSkin_Panel.transform.gameObject.SetActive(false);

        UnlockSkin(lastSkinName);
        MakeTransaction(lastSkinPrice);
        ManageAppliedSkin(lastSkinName);
        RefreshUI();


    }

    private void AdUnlockButton_Clicked()
    {

        AdsManager.instance.DisplayRewardedAd_SkinUnlock();
    }

    public void ApplySkinAdDiscount()
    {
        Debug.Log($"Unlock request for {lastSkinName} received!");

        if (isAdRewardTaskRunning) StopCoroutine(DistributeAdReward);
        DistributeAdReward = StartCoroutine(GetAdRewardTask());
    }

    private IEnumerator GetAdRewardTask()
    {

        isAdRewardTaskRunning = true;

        UnlockSkin(lastSkinName);
        MakeTransaction(discountedPrice);
        ManageAppliedSkin(lastSkinName);
        unlockSkin_Panel.transform.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        RefreshContent();

        messager.startMsg("UNLOCKED!", 1f, Vector3.zero);
        isAdRewardTaskRunning = false;
    }

    private void RefreshContent()
    {

        switch (GetSkinPath())
        {
            case "Hats":
                Tab1_Button_Clicked();
                break;
            case "Hairs":
                Tab2_Button_Clicked();
                break;
            case "Body":
                Tab3_Button_Clicked();
                break;
        }
    }

    public void ApplySkinAdDiscount_Error()
    {
        Debug.Log("An Error Occured! SkinAdUnlock");
    }

    public void SetTitleText(string txt)
    {
        titleText.text = txt;
    }



    public void SetLastClickedSkin(string name)
    {
        lastClickedSkinName = name;

        if(GetSkinPath() == "Hats" || GetSkinPath() == "Hairs")
        {
            SPrefs.SetString("LastCharacterHeadSkin", lastClickedSkinName);
        }
        else
        {
            SPrefs.SetString("LastCharacterBodySkin", lastClickedSkinName);
        }

       // Debug.Log($"Last Clicked action name {name}");
    }

    public void CheckUnequipAction(string name, Action<bool> callback)
    {

        if(name == SPrefs.GetString("PlayerHead", "none"))
        {
            UnequipHeadAccessory();
            callback(true);
        }
        else if(name == SPrefs.GetString("PlayerBody", "none"))
        {
            UnequipBodyAccessory();
            callback(true);
        }
        else
        {
            callback(false);
        }

      //  Debug.Log($"Unequip action name {name} - Got Head Pref {SPrefs.GetString("LastCharacterHeadSkin", "none")}");
    }





}
