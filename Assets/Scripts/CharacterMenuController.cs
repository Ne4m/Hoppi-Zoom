using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class CharacterMenuController : MonoBehaviour
{
    private string[] playableCharacters = {"Default Guy", "Mahmut", "Kubat", "Ozan", "Samet", "Cavo"};
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

    [Header("Character Bars")] 
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider speedBar;
    [SerializeField] private Slider ammoBar;

    [Header("Texts")]
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text titleText;


    private Image characterImg;
    private Sprite[] characterSkins;

    MainMenu_Controller mainMenu;

    void Start()
    {

        characterImg = characterOnScreen.GetComponent<Image>();
        characterSkins = Resources.LoadAll<Sprite>("Characters");
        mainMenu = GetComponent<MainMenu_Controller>();

        currentCharacterIndex = getCurrentIndex();
        updateCharacter(currentCharacterIndex);



        minCharacterIndex = 0;
        maxCharacterIndex = playableCharacters.Length-1;

        if(!(swipeLeftButton is null)) swipeLeftButton.onClick.AddListener(swipeLeftButtonClicked);
        if(!(swipeRightButton is null)) swipeRightButton.onClick.AddListener(swipeRightButtonClicked);
        if (!(selectButton is null)) selectButton.onClick.AddListener(selectButtonClicked);


    }

    private void Update()
    {
        if(Input.touchCount > 0) TouchControl(Input.touches[0]);


        if (Application.platform == RuntimePlatform.Android)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                mainMenu.canvasBackMainMenu();
            }
        }
    }

    Vector2 lastPos;
    private void TouchControl(Touch touch)
    {

        switch (touch.phase)
        {
            case TouchPhase.Began:
                lastPos = touch.position;
                break;
            case TouchPhase.Moved:
                
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Ended:

                Vector2 delta = touch.position - lastPos;

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
        titleText.text = "SELECTED CHARACTER";
        titleText.color = Color.green;
        setCurrentIndex(currentCharacterIndex);


        PlayerPrefs.SetFloat("playerHealth", characterHealth);
        PlayerPrefs.SetFloat("playerSpeed", characterSpeed);
        PlayerPrefs.SetInt("playerAmmo", characterAmmo);
        PlayerPrefs.Save();
    }

    private void swipeLeftButtonClicked()
    {
        if (currentCharacterIndex > minCharacterIndex)
        {
            currentCharacterIndex--;
            updateCharacter(currentCharacterIndex);
        }
    }

    private void swipeRightButtonClicked()
    {
        if (currentCharacterIndex < maxCharacterIndex)
        {
            currentCharacterIndex++;
            updateCharacter(currentCharacterIndex);
        }
    }

    private void updateCharacter(int index)
    {
        characterName.text = playableCharacters[index];
        characterImg.sprite = characterSkins[currentCharacterIndex];

        if (currentCharacterIndex == getCurrentIndex())
        {
            titleText.text = "SELECTED CHARACTER";
            titleText.color = Color.green;
        }
        else
        {
            titleText.text = "choose your character";
            titleText.color = Color.white;
        }

        // var number = Random.Range(0f, 1f);
        // healthBar.value = number;
        // speedBar.value = number;
        // Debug.Log($"Random Number: {number}");

        switch (index)
        {
            case 0:

                setHpByPercentage(50);
                setSpeedByPercentage(50);
                setAmmoByPercentage(0);
                break;
            case 1:

                setHpByPercentage(75);
                setSpeedByPercentage(35);

                setAmmoByPercentage(35);
                break;
            case 2:

                setHpByPercentage(100);
                setSpeedByPercentage(50);

                setAmmoByPercentage(100);
                break;
            case 3:

                setHpByPercentage(80);
                setSpeedByPercentage(30);

                setAmmoByPercentage(45);
                break;
            case 4:

                setHpByPercentage(30);
                setSpeedByPercentage(100);

                setAmmoByPercentage(45);
                break;
            case 5:

                setHpByPercentage(45);
                setSpeedByPercentage(80);

                setAmmoByPercentage(50);
                break;
        }
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
        PlayerPrefs.SetInt("LastSelectedCharacterIndex", index);
        PlayerPrefs.Save();
    }

    private int getCurrentIndex()
    {
        int tmpIndex = PlayerPrefs.GetInt("LastSelectedCharacterIndex", 0);
        return tmpIndex;
    }
}
