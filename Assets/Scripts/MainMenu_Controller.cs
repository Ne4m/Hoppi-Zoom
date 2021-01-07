using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{

    private string playerModelSkin;
    private string playerArrowSkin;
    
    // Start is called before the first frame update
    void Start()
    {
        setPlayerSkin("modelSkin", playerModelSkin);
        setPlayerSkin("arrowSkin", playerArrowSkin);
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
