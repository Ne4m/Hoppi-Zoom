using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradesController : MonoBehaviour
{
    public static UpgradesController instance;

    private PerksUpgrade lastInstance;

    [SerializeField] private TMP_Text totalCurrencyText;

    private bool isUnlockMenuActive;

    [Header ("Unlock Page Variables")]
    [SerializeField] private GameObject unlockPanel;
    [SerializeField] private Button unlockButton;
    [SerializeField] private Button adUnlockButton;
    [SerializeField] private TMP_Text unlockPerkDescription;
    [SerializeField] private Image unlockPerkImage;
    [SerializeField] private TMP_Text unlockPriceText;
    [SerializeField] private string perkName;

    private int totalCurrency;
    private int discountedPrice;

    public PerksUpgrade LastInstance
    {
        get => lastInstance;
        set => lastInstance = value;
    }

    public string PerkDescription
    {
        get => unlockPerkDescription.text;
        set => unlockPerkDescription.text = value;
    }

    public Sprite PerkImage
    {
        get => unlockPerkImage.sprite;
        set => unlockPerkImage.sprite = value;
    }

    public string PriceText
    {
        get => unlockPriceText.text;
        set => unlockPriceText.text = value;
    }

    public string PerkName
    {
        get => perkName;
        set => perkName = value;
    }


    public bool IsUnlockMenuActive
    {
        get => isUnlockMenuActive;
        set
        {
            isUnlockMenuActive = value;

            if (isUnlockMenuActive)
            {
                unlockPanel.SetActive(true);
            }
            else
            {
                unlockPanel.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {


        totalCurrency = SPrefs.GetInt("gameCurrency", 0);
        totalCurrencyText.text = totalCurrency.ToString();

        if (unlockButton != null) unlockButton.onClick.AddListener(UnlockButton_Clicked);
        if (adUnlockButton != null) adUnlockButton.onClick.AddListener(AdUnlockButton_Clicked);
    }

    private void Update()
    {
        RefreshTotalCurrencyText();
    }

    private void UnlockButton_Clicked()
    {

        ValidatePrice(valid =>
        {
            if (valid)
            {
                MakeTransaction(true, 50);
                UnlockUpgrade();
            }
            else
            {
                Debug.Log("Not Enough Currency!!");
            }
        });

    }

    private void AdUnlockButton_Clicked()
    {
        ValidatePrice(valid =>
        {
            if (valid)
            {
                AdsManager.instance.DisplayRewardedAd_UpgradeUnlock();
            }
            else
            {
                Debug.Log("Not Enough Currency!!");
            }
        });
    }

    private void ValidatePrice(Action<bool> callback)
    {
        RefreshTotalCurrencyText();

        var price = SPrefs.GetInt("perkUpgradePrice", LastInstance.NewUpgradePrice);

        if (totalCurrency >= price)
        {
            callback(true);
        }
        else
        {
            callback(false);
        }
    }

    private void MakeTransaction(bool discounted, int discountPercentage)
    {
        var price = SPrefs.GetInt("perkUpgradePrice", LastInstance.NewUpgradePrice);

        if (discounted)
        {
            discountedPrice = price - (int)(price * discountPercentage / 100);
            Debug.Log("New Discounted Upgrade PRice is " + discountedPrice);

            totalCurrency -= discountedPrice;
        }
        else
        {
            totalCurrency -= price;
        }




        price = price + (int)(price * 0.25f);

        SPrefs.SetInt("gameCurrency", totalCurrency);
        SPrefs.SetInt("perkUpgradePrice", price);
        SPrefs.Save();

        RefreshTotalCurrencyText();

        Debug.Log($"Unlocked Upgrade! {PerkName}");

    }

    private void UnlockUpgrade()
    {
        IsUnlockMenuActive = false;
        SPrefs.SetBool($"{PerkName}_locked", false);
        LastInstance.UpdateStatus(false);
    }

    private void RefreshTotalCurrencyText()
    {
        totalCurrency = SPrefs.GetInt("gameCurrency", 0);
        totalCurrencyText.text = totalCurrency.ToString();
    }

}
