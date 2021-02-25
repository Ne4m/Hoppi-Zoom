using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenuController : MonoBehaviour
{
    private string[] playableCharacters = {"Default Guy", "Mahmut", "Kubat", "Ozan", "Samet", "Cavo"};
    private string selectedCharacter;
    private int currentCharacterIndex, maxCharacterIndex, minCharacterIndex;

    [Header("Buttons")] 
    [SerializeField] private Button swipeLeftButton;
    [SerializeField] private Button swipeRightButton;
    [SerializeField] private TMP_Text characterName;


    void Awake()
    {
        currentCharacterIndex = getCurrentIndex();
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
    }
    
    private int getCurrentIndex()
    {
        // Player Prefs Stuff - Get Last Character And Return Index Based On That.. Implement Later.
        return 0;
    }
}
