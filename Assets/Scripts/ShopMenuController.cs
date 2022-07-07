using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopMenuController : MonoBehaviour
{
    [Header("Currency Text")]
    [SerializeField] private TMP_Text currencyText;


    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        currencyText.text = SPrefs.GetInt("gameCurrency", 0).ToString();
    }
}
