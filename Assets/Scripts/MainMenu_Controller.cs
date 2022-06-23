using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_Controller : MonoBehaviour
{

    private string playerModelSkin;
    private string playerArrowSkin;

    [Header("Canvas UI Buttons")] 
    [SerializeField] private Button characterMenuBack;
    [SerializeField] private Button shopMenuBack;
    
    [Header("Canvas UI Objects")] 
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private GameObject shopMenu;

    [Header ("Info Page Stuff")]
    [SerializeField] private GameObject infoPage;
    [SerializeField] private Button closeInfoPageButton;

    [Header("Middle Buttons")] 
    [SerializeField] private Button playButton;
    [SerializeField] private Button characterButton;
    [SerializeField] private Button shopButton;
    
    [Header("Down Menu IMG Resources")]
    [SerializeField] private Sprite audioOnImg;
    [SerializeField] private  Sprite audioOffImg;
    [SerializeField] private  Sprite musicOnImg;
    [SerializeField] private  Sprite musicOffImg;

    [Header("Down Menu Buttons")] 
    [SerializeField] private Button audioButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button shareButton;
    [SerializeField] private Button infoButton;
    
    
    private bool audio_ON, music_ON;


    // Start is called before the first frame update
    void Start()
    {
        setPlayerSkin("modelSkin", playerModelSkin);
        setPlayerSkin("arrowSkin", playerArrowSkin);

        // Canvas UI Elements Stuff
        canvasBackMainMenu();
        
        if(!(characterMenuBack is null)) characterMenuBack.onClick.AddListener(canvasBackMainMenu);
        if(!(shopMenuBack is null)) shopMenuBack.onClick.AddListener(canvasBackMainMenu);

        // Info Page Stuff
        if(!(closeInfoPageButton is null)) closeInfoPageButton.onClick.AddListener(closeInfoPageButtonClicked);

        // Middle Menu Stuff
        if (!(infoButton is null)) playButton.onClick.AddListener(playButtonClicked);
        if(!(infoButton is null)) characterButton.onClick.AddListener(characterButtonClicked);
        if(!(infoButton is null)) shopButton.onClick.AddListener(shopButtonClicked);
        
        
        // Down Menu Stuff
        audio_ON = true;
        music_ON = true;
        if (!(audioButton is null)) audioButton.onClick.AddListener(changeAudioButton);
        if (!(musicButton is null)) musicButton.onClick.AddListener(changeMusicButton);
        if(!(shareButton is null)) shareButton.onClick.AddListener(shareButtonClicked);
        if(!(infoButton is null)) infoButton.onClick.AddListener(infoButtonClicked);
        

    }

    void Awake()
    {
        playerModelSkin = PlayerPrefs.GetString("playerModelSkin", "Default");
        playerArrowSkin = PlayerPrefs.GetString("playerArrowSkin", "Default");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeActive_Menu(string s)
    {
        if (s == "Main")
        {
            
        }else if (s == "Shop")
        {
            
        }
        else
        {
            
        }
    }

    private void changeAudioButton()
    {
        audio_ON = !audio_ON;

        if (audio_ON)
        {
            audioButton.image.sprite = audioOnImg;
        }
        else
        {
            audioButton.image.sprite = audioOffImg;
        }

    }

    private void changeMusicButton()
    {
        music_ON = !music_ON;

        if (music_ON)
        {
            musicButton.image.sprite = musicOnImg;
        }
        else
        {
            musicButton.image.sprite = musicOffImg;
        }
        
    }

    private void closeInfoPageButtonClicked()
    {
        infoPage.SetActive(false);
    }

    private void shareButtonClicked()
    {
        print("Clicked Share Button!\n");
    }

    private void infoButtonClicked()
    {
        infoPage.SetActive(true);
        print("Clicked Info Button!\n");
    }

    private void playButtonClicked()
    {
        changeScene();
    }

    private void characterButtonClicked()
    {
        print("Clicked Character Button!");
        canvasChangeUI("character");
    }

    private void shopButtonClicked()
    {
        print("Clicked Shop Button!");
        canvasChangeUI("shop");
    }

    private void setPlayerSkin(string type, string skinName)
    {
        if (type == "modelSkin")
        {
            PlayerPrefs.SetString("playerModelSkin", skinName);
            // Change Canvas Stuff HERE
        }
        else if (type == "arrowSkin")
        {
            PlayerPrefs.SetString("playerSkin", skinName);
            // Change Canvas Stuff HERE
        }
    }



    private void canvasChangeUI(string IN)
    {
        switch (IN)
        {
            case "main":
                mainMenu.SetActive(true);
                characterMenu.SetActive(false);
                shopMenu.SetActive(false);
                break;
            case "character":
                mainMenu.SetActive(false);
                characterMenu.SetActive(true);
                shopMenu.SetActive(false);
                break;
            case "shop":
                mainMenu.SetActive(false);
                characterMenu.SetActive(false);
                shopMenu.SetActive(true);
                break;
        }
    }

    private void canvasBackMainMenu()
    {
        canvasChangeUI("main");
    }

    private void changeScene()
    {
        SceneManager.LoadScene("Game");
    }
}
