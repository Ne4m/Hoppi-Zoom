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

    private float baseHealth = 2000;
    private float baseSpeed = 1000;

    [SerializeField] private Transform characterOnScreen;

    [Header("Buttons")] 
    [SerializeField] private Button swipeLeftButton;
    [SerializeField] private Button swipeRightButton;
    [SerializeField] private Button selectButton;

    [Header("Character Bars")] 
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider speedBar;

    [Header("Texts")]
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text titleText;


    private Image characterImg;
    private Sprite[] characterSkins;


    void Start()
    {
        characterImg = characterOnScreen.GetComponent<Image>();
        characterSkins = Resources.LoadAll<Sprite>("Characters");

        currentCharacterIndex = getCurrentIndex();
        updateCharacter(currentCharacterIndex);



        minCharacterIndex = 0;
        maxCharacterIndex = playableCharacters.Length-1;

        if(!(swipeLeftButton is null)) swipeLeftButton.onClick.AddListener(swipeLeftButtonClicked);
        if(!(swipeRightButton is null)) swipeRightButton.onClick.AddListener(swipeRightButtonClicked);
        if (!(selectButton is null)) selectButton.onClick.AddListener(selectButtonClicked);


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
                break;
            case 1:

                setHpByPercentage(75);
                setSpeedByPercentage(35);
                break;
            case 2:

                setHpByPercentage(100);
                setSpeedByPercentage(50);

                break;
            case 3:

                setHpByPercentage(80);
                setSpeedByPercentage(30);
                break;
            case 4:

                setHpByPercentage(30);
                setSpeedByPercentage(100);
                break;
            case 5:

                setHpByPercentage(100);
                setSpeedByPercentage(100);
                break;
        }
    }

    private void adjustCharacterStats(int type, float percentage)
    {
        if(type == 0)
        {
            characterHealth = baseHealth * (percentage / 100);
        }
        else if(type == 1)
        {
            characterSpeed = baseSpeed * (percentage / 100);
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
