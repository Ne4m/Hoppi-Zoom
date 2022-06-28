using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;
    [SerializeField] private Button selectButton;

    private bool anyBtnClicked;

    void Start()
    {
        if (btn1 != null) btn1.onClick.AddListener(btn1_Clicked);
        if (btn2 != null) btn2.onClick.AddListener(btn2_Clicked);
        if (selectButton != null) selectButton.onClick.AddListener(selectButton_Clicked);
    }

    private void Update()
    {


        if (Application.platform == RuntimePlatform.Android)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                int index = SceneManager.GetActiveScene().buildIndex - 1;

                if(index > -1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);

            }
        }

    }

    private void btn1_Clicked()
    { // 229 213 33
        btn1.image.color = new Color32(229, 213, 33, 255);
        btn2.image.color = Color.white;
        anyBtnClicked = true;
        PlayerPrefs.SetInt("ControlInput", 0);
    }

    private void btn2_Clicked()
    {
        btn1.image.color = Color.white;
        btn2.image.color = new Color32(229, 213, 33, 255);
        anyBtnClicked = true;
        PlayerPrefs.SetInt("ControlInput", 1);
    }

    private void selectButton_Clicked()
    {
        if (!anyBtnClicked) return;

        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }
}
