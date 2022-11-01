using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialsManager : MonoBehaviour
{
    [Header("Tutorials")]
    [SerializeField] private Tutorial[] tutorial;

    private GameObject[] instantiatedTutorialObj;

    [Header("Tutorial Objects")]


    public static TutorialsManager instance;

    private void Awake()
    {
        instance = this;
        instantiatedTutorialObj = new GameObject[tutorial.Length];
    }

    private void Start()
    {

        for (int i = 0; i < tutorial.Length; i++)
        {
            SPrefs.SetBool($"isTutorial_{i}_Done", false);
        }

        for (int i=0; i < tutorial.Length; i++)
        {

            if (IsTutorialDone(i))
            {
                SetTutorialAsDone(i);
            }
        }

    }



    public void PlayTutorial(int n)
    {
        if(!IsTutorialDone(n))
        {

            instantiatedTutorialObj[n] = Instantiate(tutorial[n].GetPrefab(), Vector3.zero, Quaternion.identity);
            instantiatedTutorialObj[n].transform.localScale *= Sprite_AutoRes.instance.scaleRatio;

            Canvas canvas = instantiatedTutorialObj[n].transform.GetChild(0).GetComponent<Canvas>();
            TMP_Text text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
            //text.transform.localScale *= Sprite_AutoRes.instance.scaleRatio;
            text.text = I18n.Fields[$"T_TUTORIAL{n+1}_TEXT"];
        }
            
    }

    public void EndTutorial(int n)
    {
        if(instantiatedTutorialObj[n] != null)
        {
            Destroy(instantiatedTutorialObj[n]);
            SetTutorialAsDone(n);
        }
    }

    public bool IsTutorialDone(int n)
    {
        var status = SPrefs.GetBool($"isTutorial_{n}_Done", false);
        Debug.Log($"Tutorial No:{n} Status {status}");
        return status;
    }

    public void SetTutorialAsDone(int n)
    {
        tutorial[n].SetTutorialAsDone();
        SPrefs.SetBool($"isTutorial_{n}_Done", true);
    }


    [System.Serializable]
    public class Tutorial
    {

        [SerializeField] private int tutorialID;
        [SerializeField] private string tutorialName;
        [SerializeField] private string tutorialDescription;
        [SerializeField] private bool isTutorialDone = false;
        [SerializeField] private GameObject tutorialPrefab;

        public void SetTutorialAsDone()
        {
            this.isTutorialDone = true;
        }

        public GameObject GetPrefab()
        {
            return this.tutorialPrefab;
        }
    }
}
