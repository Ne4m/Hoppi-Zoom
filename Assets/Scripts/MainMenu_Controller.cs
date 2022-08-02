using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using TMPro;


public class MainMenu_Controller : MonoBehaviour
{

    private string playerModelSkin;
    private string playerArrowSkin;

    [Header("Canvas UI Buttons")] 
    [SerializeField] private Button characterMenuBack;
    [SerializeField] private Button shopMenuBack;

    [Header("Quit Prompt Dialogue")]
    [SerializeField] GameObject quitPromptContainer;
    [SerializeField] TMP_Text quitGameText;
    [SerializeField] private Button yesBtn, noBtn;
    private bool isAtMainMenu;
    
    [Header("Canvas UI Objects")] 
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject accessoryMenu;

    [Header ("Info Page Stuff")]
    [SerializeField] private GameObject infoPage;
    [SerializeField] private Button closeInfoPageButton;

    [Header("Middle Buttons")] 
    [SerializeField] private Button playButton;
    [SerializeField] private Button characterButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button accessoriesButton;
    
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

    [Header("Username Google Play Test")]
    [SerializeField] private TMP_Text usernameTxt;
    
    private bool audio_ON, music_ON;

    PlayGamesPlatform platform;
    public static MainMenu_Controller instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // Canvas UI Elements Stuff
        //canvasBackMainMenu();
        
        if(!(characterMenuBack is null)) characterMenuBack.onClick.AddListener(canvasBackMainMenu);
        if(!(shopMenuBack is null)) shopMenuBack.onClick.AddListener(canvasBackMainMenu);

        // Quit Prompt Dialogue Stuff
        if (yesBtn is not null) yesBtn.onClick.AddListener(yesBtn_Clicked);
        if (noBtn is not null) noBtn.onClick.AddListener(noBtn_Clicked);

        // Info Page Stuff
        if(!(closeInfoPageButton is null)) closeInfoPageButton.onClick.AddListener(closeInfoPageButtonClicked);

        // Middle Menu Stuff
        if (!(playButton is null)) playButton.onClick.AddListener(playButtonClicked);
        if(!(characterButton is null)) characterButton.onClick.AddListener(characterButtonClicked);
        if(!(shopButton is null)) shopButton.onClick.AddListener(shopButtonClicked);
        if (!(accessoriesButton is null)) accessoriesButton.onClick.AddListener(accessoryButtonClicked);

        // Down Menu Stuff
        audio_ON = true;
        music_ON = true;
        if (!(audioButton is null)) audioButton.onClick.AddListener(changeAudioButton);
        if (!(musicButton is null)) musicButton.onClick.AddListener(changeMusicButton);
        if(!(shareButton is null)) shareButton.onClick.AddListener(shareButtonClicked);
        if(!(infoButton is null)) infoButton.onClick.AddListener(infoButtonClicked);

        PlayGamesPlatform.DebugLogEnabled = true;
        platform = PlayGamesPlatform.Activate();

        FindObjectOfType<AudioManager>().Play("Main");
    }


    // Update is called once per frame
    void Update()
    {


          //  usernameTxt.text = ($"({platform.IsAuthenticated()} {platform.GetUserDisplayName()})");


        //if (Application.platform == RuntimePlatform.Android)
        //{


        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isAtMainMenu) ExitGameConfirmation();
        }
    }

    void ExitGameConfirmation()
    {
        quitPromptContainer.gameObject.SetActive(true);
    }

    private void yesBtn_Clicked()
    {
        Application.Quit();
    }

    private void noBtn_Clicked()
    {
        quitPromptContainer.gameObject.SetActive(false);
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
            FindObjectOfType<AudioManager>().VolumeControl("Main", 0.25f, 1.0f);
        }
        else
        {
            musicButton.image.sprite = musicOffImg;
            FindObjectOfType<AudioManager>().VolumeControl("Main", 0f, 1.0f);
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

    private void accessoryButtonClicked()
    {
        canvasChangeUI("accessory");
    }


    private void canvasChangeUI(string IN)
    {

        AdsManager.instance.MainMenuActiveUI = IN;

        switch (IN)
        {
            case "main":
                mainMenu.SetActive(true);
                characterMenu.SetActive(false);
                shopMenu.SetActive(false);
                accessoryMenu.SetActive(false);
                isAtMainMenu = true;
                break;
            case "character":
                mainMenu.SetActive(false);
                characterMenu.SetActive(true);
                shopMenu.SetActive(false);
                accessoryMenu.SetActive(false);
                isAtMainMenu = false;
                break;
            case "shop":
                mainMenu.SetActive(false);
                characterMenu.SetActive(false);
                shopMenu.SetActive(true);
                accessoryMenu.SetActive(false);
                isAtMainMenu = false;
                break;
            case "accessory":
                mainMenu.SetActive(false);
                characterMenu.SetActive(false);
                shopMenu.SetActive(false);
                accessoryMenu.SetActive(true);
                accessoryMenu.TryGetComponent<SkinManager>(out SkinManager skinManager);
                skinManager.RefreshUI();
                isAtMainMenu = false;
                break;
        }

        if (!isAtMainMenu)
        {
            noBtn_Clicked();
        }
    }

    public void canvasBackMainMenu()
    {
        canvasChangeUI("main");
    }

    private void changeScene()
    {
        SceneManager.LoadScene("InputSelect");
    }
}
