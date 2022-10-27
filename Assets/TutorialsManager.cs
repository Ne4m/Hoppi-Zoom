using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialsManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool tutorialDone = false;

    [Header("Tutorial Objects")]
    [SerializeField] private GameObject touchToStartObj;
    private GameObject touchToStart;

    public static TutorialsManager instance;

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {

        switch (!tutorialDone)
        {
            case true:
                ShowFirstTutorial();
                    break;
            case false:
                EndFirstTutorial();
                    break;
        }
    }

    public void ShowFirstTutorial()
    {
        if(touchToStartObj != null)
        {
            touchToStart = Instantiate(touchToStartObj, Vector3.zero, Quaternion.identity);
            touchToStart.transform.localScale *= Sprite_AutoRes.instance.scaleRatio;

            Canvas canvas = touchToStart.transform.GetChild(1).GetComponent<Canvas>();
            TMP_Text text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
            text.transform.localScale *= Sprite_AutoRes.instance.scaleRatio;
        }
    }

    public void EndFirstTutorial()
    {
        if(touchToStart != null && !tutorialDone)
        {
            Destroy(touchToStart);
            tutorialDone = true;
        }
    }
}
