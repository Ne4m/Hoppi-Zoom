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

        int spritePrice = 0;
        if (transform.GetChild(1).transform.gameObject.activeSelf)
        {
            spritePrice = Convert.ToInt32(transform.GetChild(1).GetComponent<TMP_Text>().text);
            SPrefs.SetInt("LastClickedSkinPrice", spritePrice);
            SPrefs.Save();
        }

        var _sprite = transform.GetChild(2).GetComponent<Image>().sprite;
        var spriteName = _sprite.name;
        SPrefs.SetString("LastClickedSkin", spriteName);
        skinManager.SetLastClickedSprite(_sprite);

        skinManager.SetTitleText(spriteName);

        skinManager.CheckUnequipAction(spriteName, unequipped =>
        {
            if (!unequipped)
            {
                skinManager.ManageAppliedSkin(spriteName);
                skinManager.RefreshUI();
            }
        });

        

        //skinManager.SetLastClickedSkin(spriteName);




    }
}
