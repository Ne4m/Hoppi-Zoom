using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPanelAutoHide : MonoBehaviour
{
    public static UnlockPanelAutoHide instance;
    private GameObject unlockPanel;

    private void Awake()
    {

        instance = this;
        unlockPanel = gameObject;

        MainMenu_Controller.instance.autoHideInstance.Add(instance);


    }


    public bool UnlockPanelVisibility
    {
       set
        {
            unlockPanel.SetActive(value);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
