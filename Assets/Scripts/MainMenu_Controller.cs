using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_Controller : MonoBehaviour
{

    private string playerModelSkin;
    private string playerArrowSkin;

    [SerializeField] private Sprite audio_ON_IMG, audio_OFF_IMG;
    [SerializeField] private Sprite music_ON_IMG, music_OFF_IMG;

    [SerializeField] private Button audioButton;
    [SerializeField] private Button musicButton;
    private bool audio_ON;
    private bool music_ON;

    
    // Start is called before the first frame update
    void Start()
    {
        setPlayerSkin("modelSkin", playerModelSkin);
        setPlayerSkin("arrowSkin", playerArrowSkin);

        audio_ON = true;
        music_ON = true;
        
        

        if (!(audioButton is null)) audioButton.onClick.AddListener(changeAudio);
        if (!(musicButton is null)) musicButton.onClick.AddListener(changeMusic);
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

    private void changeAudio()
    {
        audio_ON = !audio_ON;

        if (audio_ON)
        {
            audioButton.image.sprite = audio_ON_IMG;
        }
        else
        {
            audioButton.image.sprite = audio_OFF_IMG;
        }
    }

    private void changeMusic()
    {
        music_ON = !music_ON;

        if (music_ON)
        {
            musicButton.image.sprite = music_ON_IMG;
        }
        else
        {
            musicButton.image.sprite = music_OFF_IMG;
        }
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
    
    


    public void changeScene()
    {
        SceneManager.LoadScene("Game");
    }
}
