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
    private string selectedCharacter;
    private int currentCharacterIndex, maxCharacterIndex, minCharacterIndex;
    private int characterHealth, characterSpeed;

    [Header("Buttons")] 
    [SerializeField] private Button swipeLeftButton;
    [SerializeField] private Button swipeRightButton;
    [SerializeField] private TMP_Text characterName;

    [Header("Character Bars")] 
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider speedBar;


    void Awake()
    {
        currentCharacterIndex = getCurrentIndex();
        updateCharacter(currentCharacterIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        minCharacterIndex = 0;
        maxCharacterIndex = playableCharacters.Length-1;

        if(!(swipeLeftButton is null)) swipeLeftButton.onClick.AddListener(swipeLeftButtonClicked);
        if(!(swipeRightButton is null)) swipeRightButton.onClick.AddListener(swipeRightButtonClicked);
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

        // var number = Random.Range(0f, 1f);
        // healthBar.value = number;
        // speedBar.value = number;
        // Debug.Log($"Random Number: {number}");

        switch (index)
        {
            case 0:
                print($"Case is 0 Expected Char Name is -Default Guy- is it tho ? {characterName.text}");
                setHpByPercentage(50);
                setSpeedByPercentage(50);
                print($"HP : {healthBar.value*100} - Speed: {speedBar.value*100}");
                break;
            case 1:
                print($"Case is 1 Expected Char Name is -Mahmut- is it tho ? {characterName.text}");
                setHpByPercentage(75);
                setSpeedByPercentage(35);
                print($"HP : {healthBar.value*100} - Speed: {speedBar.value*100}");
                break;
            case 2:
                print($"Case is 2 Expected Char Name is -Kubat- is it tho ? {characterName.text}");
                setHpByPercentage(100);
                setSpeedByPercentage(50);
                print($"HP : {healthBar.value*100} - Speed: {speedBar.value*100}");
                break;
            case 3:
                print($"Case is 3 Expected Char Name is -Ozan- is it tho ? {characterName.text}");
                setHpByPercentage(80);
                setSpeedByPercentage(30);
                print($"HP : {healthBar.value*100} - Speed: {speedBar.value*100}");
                break;
            case 4:
                print($"Case is 4 Expected Char Name is -Samet- is it tho ? {characterName.text}");
                setHpByPercentage(30);
                setSpeedByPercentage(100);
                print($"HP : {healthBar.value*100} - Speed: {speedBar.value*100}");
                break;
            case 5:
                print($"Case is 5 Expected Char Name is -Cavo- is it tho ? {characterName.text}");
                setHpByPercentage(100);
                setSpeedByPercentage(100);
                print($"HP : {healthBar.value*100} - Speed: {speedBar.value*100}");
                break;
        }
    }


    private void setHpByPercentage(float val)
    {
        if (val < 0) val = 0;
        if (val > 100) val = 100;
        
        
        healthBar.value = val / 100;
    }
    
    private void setSpeedByPercentage(float val)
    {
        if (val < 0) val = 0;
        if (val > 100) val = 100;
        
        
        speedBar.value = val / 100;
    }
    

    private int getCurrentIndex()
    {
        // Player Prefs Stuff - Get Last Character And Return Index Based On That.. Implement Later.
        return 0;
    }
}
