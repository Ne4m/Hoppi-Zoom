using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

public class CharacterMenuController : MonoBehaviour
{
    private string[] playableCharacters = {"Blue (Default)", "Green", "Grey", "Orange", "Purple", "Red", "Teal", "Yellow" }; //  "NewChar 1", "NewChar 2", "NewChar 3", "NewChar 4", "NewChar 5", "NewChar 6", "NewChar 7" 
    private string[] unlockedCharacters;
    private int currentCharacterIndex, maxCharacterIndex, minCharacterIndex;
    private float characterHealth, characterSpeed;
    private int characterAmmo;

    private float baseHealth = 1000;
    private float baseSpeed = 400;
    private int baseAmmo = 3;

    //[Header ("Tuple")]
    //[SerializeField] public (float Health, int Ammo, string name) Character1, Character2, Character3;

    [SerializeField] private Transform characterOnScreen;

    [Header("Buttons")] 
    [SerializeField] private Button swipeLeftButton;
    [SerializeField] private Button swipeRightButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button addCurrencyButton;


    [Header("Character Bars")] 
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider speedBar;
    [SerializeField] private Slider ammoBar;

    [Header("Texts")]
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text currencyText;

    [Header("Perks Window Properties")]
    [SerializeField] private GameObject perksContainer;
    [SerializeField] private TMP_Text perksDescription;
    [SerializeField] private Image perksImage;
    [SerializeField] private Button perksImageBtn;

    [Header ("Unlock Character Properties")]
    private int baseSkinPrice = 250;
    private double unlockMultiplier = 0.20;
    [SerializeField] private Button unlockButton;
    [SerializeField] private TMP_Text unlockCost;
    [SerializeField] private GameObject unlockPriceContainer;

    [Header("Unlock Confirmation Dialogue")]
    [SerializeField] private GameObject unlockConfirmationContainer;
    [SerializeField] private TMP_Text oldPrice;
    [SerializeField] private TMP_Text newPrice;
    [SerializeField] private Button unlockConfirmation_YesBtn;
    [SerializeField] private Button unlockConfirmation_NoBtn;

    private bool isUnlockContainerActive;
    private UIMessager messager;


    private Coroutine showPerkDescription;
    private bool isShowing = false;


    private Image characterImg;
    private Sprite[] characterSkins;

    private PerkList lastPerk;

    MainMenu_Controller mainMenu;


    public string PerksDescription
    {
        get => perksDescription.text;
        set
        {
            perksDescription.text = "<mark=#F3930079>" + value + "</mark>";
        }
    }

    public string PerksImage
    {
        get => perksImage.sprite.name;
        set
        {
            if(perksImage.gameObject.activeSelf)
                perksImage.sprite = Resources.Load<Sprite>($"Perks/{value}");
        }
    }

    private void Awake()
    {
        //baseSkinPrice = SPrefs.GetInt("currentBaseSkinPrice", baseSkinPrice);
    }

    void Start()
    {



        messager = GetComponent<UIMessager>();

        if (perksImageBtn != null) perksImageBtn.onClick.AddListener(perksImgBtn_Clicked);

        if (unlockConfirmation_YesBtn != null) unlockConfirmation_YesBtn.onClick.AddListener(UnlockConfirmation_YesBtn_Clicked);
        if (unlockConfirmation_NoBtn != null) unlockConfirmation_NoBtn.onClick.AddListener(UnlockConfirmation_NoBtn_Clicked);

        if (addCurrencyButton != null) addCurrencyButton.onClick.AddListener(() =>
        {
            MainMenu_Controller.instance.ChangeCanvas("shop");
        });

        characterImg = characterOnScreen.GetComponent<Image>();
        characterSkins = Resources.LoadAll<Sprite>("Characters");
        mainMenu = MainMenu_Controller.instance;
        unlockedCharacters = new string[characterSkins.Length];


        //Default Character
        SPrefs.SetString("character_" + 0, "unlocked");
        SPrefs.Save();


        

        for (int i = 0; i < characterSkins.Length; i++)
        {
            unlockedCharacters[i] = SPrefs.GetString("character_" + i, "locked");
        }




        currentCharacterIndex = getCurrentIndex();
        updateCharacter(currentCharacterIndex);



        minCharacterIndex = 0;
        maxCharacterIndex = playableCharacters.Length-1;

        if(!(swipeLeftButton is null)) swipeLeftButton.onClick.AddListener(swipeLeftButtonClicked);
        if(!(swipeRightButton is null)) swipeRightButton.onClick.AddListener(swipeRightButtonClicked);
        if (!(selectButton is null)) selectButton.onClick.AddListener(selectButtonClicked);
        if (!(unlockButton is null)) unlockButton.onClick.AddListener(unlockButtonClicked);



        /// REMOVE ON LAUNCH
        /// 
        SPrefs.SetInt("gameCurrency", 2500);
        LockAllCharacters();
        SPrefs.SetInt("currentBaseSkinPrice", baseSkinPrice);
        Debug.LogWarning("Lock All Characters Is Active @CharacterMenuController.cs Start !!!");
        /// 
    }

    private void LockAllCharacters()
    {
        for (int i = 1; i < characterSkins.Length; i++)
        {
            SPrefs.SetString("character_" + i, "locked");
        }
        SPrefs.Save();
    }
   

    private void RefreshCurrency()
    {
        currencyText.text = SPrefs.GetInt("gameCurrency", 0).ToString();

        unlockCost.text = baseSkinPrice.ToString();

        int number = Convert.ToInt32(currencyText.text);
        if (number > 9999999)
        {
            // Cheating Flush the whole currency & quit.
            SPrefs.SetInt("gameCurrency", 0);
            Application.Quit();
        }
    }

    private void Update()
    {
        if(Input.touchCount > 0) TouchControl(Input.GetTouch(0));


        //if (Application.platform == RuntimePlatform.Android)
        //{

        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.canvasBackMainMenu();
        }

        isUnlockContainerActive = unlockConfirmationContainer.gameObject.activeSelf;
        RefreshCurrency();

    }

    Vector2 initialPos = Vector2.zero;
    Vector2 lastPos = Vector2.zero;

    private void TouchControl(Touch touch)
    {


        switch (touch.phase)
        {
            case TouchPhase.Began:
                initialPos = touch.position;
                break;
            case TouchPhase.Moved:
                
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Ended:

                lastPos = touch.position;

                Vector2 delta = (lastPos - initialPos).normalized;

                if (delta.x < 0) // Swiping Left!
                {
                    swipeRightButtonClicked();
                }
                else if(delta.x > 0) // Swiping Right!
                {
                    swipeLeftButtonClicked(); 
                }
                break;
            case TouchPhase.Canceled:
                break;
        }
    }

    private void selectButtonClicked()
    {
        // 
        Debug.Log($"Current Index = {currentCharacterIndex}");
        titleText.text = I18n.Fields["T_CHARACTER_TITLE_SELECTED"];
        titleText.color = Color.green;
        setCurrentIndex(currentCharacterIndex);

        // bronu s2m

        SPrefs.SetFloat("playerHealth", characterHealth);
        SPrefs.SetFloat("playerSpeed", characterSpeed);
        SPrefs.SetInt("playerAmmo", characterAmmo);
        SPrefs.Save();
    }

    private void unlockButtonClicked()
    {
        if (unlockedCharacters[currentCharacterIndex] == "unlocked") return;



        RequestCharacterUnlock();

    }

    private void RequestCharacterUnlock()
    {
        baseSkinPrice = SPrefs.GetInt("currentBaseSkinPrice", baseSkinPrice);

        var currency = SPrefs.GetInt("gameCurrency", 0);

        if (currency >= baseSkinPrice)
        {
            oldPrice.text = baseSkinPrice.ToString();
            newPrice.text = (baseSkinPrice / 2).ToString();
            unlockConfirmationContainer.gameObject.SetActive(true);
        }
        else
        {
            messager.startMsgv2(I18n.Fields["T_INSUFFICIENT_STARS"], 2f, Vector3.zero, Color.red);
            AudioManager.instance.IsPlaying("Insufficient", (cb) =>
            {
                if (!cb)
                {
                    AudioManager.instance.Play("Insufficient");
                }
            });
        }
    }

    private void UnlockConfirmation_YesBtn_Clicked()
    {
        AdsManager.instance.DisplayRewardedAd_CharacterUnlock();

    }

    private void UnlockConfirmation_NoBtn_Clicked()
    {


        ManageUnlockPrice(baseSkinPrice, (isDone) =>
        {
            if (isDone)
            {
                UnlockCharacter(currentCharacterIndex);
                updateCharacter(currentCharacterIndex);
                unlockConfirmationContainer.gameObject.SetActive(false);

            }
        });


    }

    public void ApplyAdDiscount()
    {
        ManageUnlockPrice((int) (baseSkinPrice * 0.5), (isDone) =>
        {
            if (isDone)
            {
                UnlockCharacter(currentCharacterIndex);
                updateCharacter(currentCharacterIndex);

                unlockConfirmationContainer.gameObject.SetActive(false);

            }
        });

    }

    public void ApplyAdDiscount_Error()
    {
        messager.startMsgv2(I18n.Fields["T_AN_ERROR_OCCURED"], 1f, Vector3.zero, Color.red);
    }

    private void ManageUnlockPrice(int price, Action<bool> callback)
    {
        var balance = SPrefs.GetInt("gameCurrency", 0);

        balance -= price;

        SPrefs.SetInt("gameCurrency", balance);
        SPrefs.Save();

        messager.startMsgv2(I18n.Fields["T_CHARACTER_UNLOCKED"], 1f, Vector3.zero, Color.green);
        callback(true);
    }

    private void swipeLeftButtonClicked()
    {
        if (!isUnlockContainerActive)
        {
            if (currentCharacterIndex > minCharacterIndex)
            {
                currentCharacterIndex--;
                updateCharacter(currentCharacterIndex);
            }
        }
    }

    private void swipeRightButtonClicked()
    {
        if (!isUnlockContainerActive)
        {
            if (currentCharacterIndex < maxCharacterIndex)
            {
                currentCharacterIndex++;
                updateCharacter(currentCharacterIndex);
            }
        }

    }

    private void updateCharacter(int index)
    {
        characterName.text = playableCharacters[index];
        characterImg.sprite = characterSkins[currentCharacterIndex];

        Debug.Log($"Character Info: {unlockedCharacters[index]}");

        if (unlockedCharacters[index] == "locked")
        {
            //characterImg.color = Color.black;
            titleText.text = I18n.Fields["T_CHARACTER_TITLE_LOCKED"]; // "LOCKED CHARACTER";
            titleText.color = Color.grey;

            selectButton.gameObject.SetActive(false);
            //unlockButton.gameObject.SetActive(true);
            unlockPriceContainer.gameObject.SetActive(true);
        }
        else
        {
            characterImg.color = Color.white;
            titleText.text = I18n.Fields["T_CHARACTER_TITLE_CHOOSE"]; //"choose your character";
            titleText.color = Color.white;

            selectButton.gameObject.SetActive(true);
            //unlockButton.gameObject.SetActive(false);
            unlockPriceContainer.gameObject.SetActive(false);
        }

        if (currentCharacterIndex == getCurrentIndex())
        {
            titleText.text = I18n.Fields["T_CHARACTER_TITLE_SELECTED"]; //"SELECTED CHARACTER";
            titleText.color = Color.green;

        }
        //else
        //{
        //    titleText.text = "choose your character";
        //    titleText.color = Color.white;
        //}

        // var number = Random.Range(0f, 1f);
        // healthBar.value = number;
        // speedBar.value = number;
        // Debug.Log($"Random Number: {number}");

        if (!perksContainer.gameObject.activeSelf) perksContainer.gameObject.SetActive(true);

        switch (index)
        {
            case 0:

                setHpByPercentage(50);
                setSpeedByPercentage(50);
                setAmmoByPercentage(0);

                Perks.instance.SetActivePerk(PerkList.DEFAULT_NONE);
                lastPerk = PerkList.DEFAULT_NONE;

                perksContainer.gameObject.SetActive(false);
                break;
            case 1:

                setHpByPercentage(75);
                setSpeedByPercentage(35);
                setAmmoByPercentage(35);

                Perks.instance.SetSelectedCharacterPerk(PerkList.FASTER_BULLETS);
                lastPerk = PerkList.FASTER_BULLETS;
                break;
            case 2:

                setHpByPercentage(100);
                setSpeedByPercentage(50);
                setAmmoByPercentage(100);

                Perks.instance.SetSelectedCharacterPerk(PerkList.AMMO_RECHARGE);
                lastPerk = PerkList.AMMO_RECHARGE;
                break;
            case 3:

                setHpByPercentage(80);
                setSpeedByPercentage(30);
                setAmmoByPercentage(45);

                Perks.instance.SetSelectedCharacterPerk(PerkList.MOVE_HORIZONTAL);
                lastPerk = PerkList.MOVE_HORIZONTAL;
                break;
            case 4:

                setHpByPercentage(30);
                setSpeedByPercentage(100);

                setAmmoByPercentage(45);

                Perks.instance.SetSelectedCharacterPerk(PerkList.TAKE_LESS_DAMAGE);
                lastPerk = PerkList.TAKE_LESS_DAMAGE;
                break;
            case 5:

                setHpByPercentage(75);
                setSpeedByPercentage(40);

                setAmmoByPercentage(100);

                Perks.instance.SetSelectedCharacterPerk(PerkList.CHEAT_DEATH);
                lastPerk = PerkList.CHEAT_DEATH;
                break;
            case 6:

                setHpByPercentage(45);
                setSpeedByPercentage(80);

                setAmmoByPercentage(50);

                Perks.instance.SetSelectedCharacterPerk(PerkList.CHANCE_TO_HEAL_ON_HIT);
                lastPerk = PerkList.CHANCE_TO_HEAL_ON_HIT;
                break;

            case 7:

                setHpByPercentage(50);
                setSpeedByPercentage(80);

                setAmmoByPercentage(50);

                Perks.instance.SetSelectedCharacterPerk(PerkList.LONGER_GRACE_PERIOD);
                lastPerk = PerkList.LONGER_GRACE_PERIOD;
                break;
        }

        
        PerksImage = Perks.instance.LastPerkImage; //SPrefs.GetString("LastPerkImageName", "none");
    }

    private void perksImgBtn_Clicked()
    {
        if(isShowing && showPerkDescription != null)
        {
            StopCoroutine(showPerkDescription);
        }

        showPerkDescription = StartCoroutine(ShowPerksDescription());


    }

    private IEnumerator ShowPerksDescription()
    {
        isShowing = true;

        PerksDescription = Perks.instance.LastPerkDescription;
        perksDescription.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.25f);

        perksDescription.gameObject.SetActive(false);
        isShowing = false;
    }

    private void adjustCharacterStats(int type, float percentage)
    {
        if(type == 0) // Health
        {
            characterHealth = baseHealth + (baseHealth * (percentage / 100));
        }
        else if(type == 1) // Speed
        {
            characterSpeed = baseSpeed + (baseSpeed * (percentage / 100));
        }
        else if (type == 2) // Ammo
        {
            characterAmmo = baseAmmo + (baseAmmo * ((int)percentage / 100));
        }

    }


    private void setHpByPercentage(float val)
    {
        if (val < 0) val = 0;
        if (val > 100) val = 100;
        
        
        healthBar.value = val / 100;

        adjustCharacterStats(0, val);
    }
    
    private void setSpeedByPercentage(float val)
    {
        if (val < 0) val = 0;
        if (val > 100) val = 100;
        
        
        speedBar.value = val / 100;

        adjustCharacterStats(1, val);
    }

    private void setAmmoByPercentage(float val)
    {
        if (val < 0) val = 0;
        if (val > 100) val = 100;

        ammoBar.value = val / 100;

        adjustCharacterStats(2, val);
    }
    

    private void setCurrentIndex(int index)
    {
        SPrefs.SetInt("LastSelectedCharacterIndex", index);
        SPrefs.SetString("LastSelectedPerk", lastPerk.ToString());
        SPrefs.Save();
    }

    private int getCurrentIndex()
    {
        int tmpIndex = SPrefs.GetInt("LastSelectedCharacterIndex", 0);
        return tmpIndex;
    }

    private void UnlockCharacter(int index)
    {
        unlockedCharacters[index] = "unlocked";
        SPrefs.SetString("character_" + index, "unlocked");
      //  SPrefs.Save();

        baseSkinPrice += (int)(baseSkinPrice * unlockMultiplier);
        SPrefs.SetInt("currentBaseSkinPrice", baseSkinPrice);
        SPrefs.Save();
    }


}
