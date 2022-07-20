using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ApplySkin : MonoBehaviour
{
    [SerializeField] private GameObject accessoriesContainer;
    [SerializeField] private TMP_Text skinNameText;
    private Button applyBtn;

    SkinManager skinManager;

    void Start()
    {

        skinManager = SkinManager.instance;

        applyBtn = GetComponent<Button>();

        accessoriesContainer = GameObject.Find("@ACCESSORIES");
        skinNameText = accessoriesContainer.transform.GetChild(2).GetComponent<TMP_Text>();
        if (applyBtn != null) applyBtn.onClick.AddListener(ApplyBtn_Clicked);
    }

    
    void ApplyBtn_Clicked()
    {
        //skinNameText.text = transform.GetChild(2).GetComponent<Image>().sprite.name;

        int spritePrice = Convert.ToInt32(transform.GetChild(1).GetComponent<TMP_Text>().text);
        var spriteName = transform.GetChild(2).GetComponent<Image>().sprite.name;

        skinManager.SetTitleText(spriteName);

        SPrefs.SetInt("CurrentSkinPrice", spritePrice);
    }
}
