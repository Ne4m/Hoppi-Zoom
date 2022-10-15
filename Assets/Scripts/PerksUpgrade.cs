using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class PerksUpgrade : MonoBehaviour
{
    private bool isActive = false;
    private int basePrice = 1000;
    private int newPrice = 1000;

    public int NewUpgradePrice
    {
        get => newPrice;
        set => newPrice = value;
    }


    [SerializeField] private Button chooseButton;
    [SerializeField] private UnityEvent onChoose;

    [Header("Child Initializations")]
    private Transform _transform;
    [SerializeField] private PerkList perk;
    private string perkName;
    [SerializeField] private Sprite statusImage;
    private Sprite perkImage;
    [SerializeField] private string perkDescription;
    private int price;
    [SerializeField] private bool isLocked = true;
    private GameObject priceText;
    private GameObject goldImage;
    private GameObject perkDesc;


    [Header ("Lazy Stuff")]
    [SerializeField] private Sprite statusOnImg;
    [SerializeField] private Sprite statusOffImg;
    [SerializeField] private Sprite statusLockedImg;


    void Start()
    {


        perkName = perk.ToString();

        isLocked = SPrefs.GetBool($"{perkName}_locked", true);
        isActive = SPrefs.GetBool($"{perkName}_activated", false);


        NewUpgradePrice = SPrefs.GetInt("perkUpgradePrice", basePrice);

        if (!isLocked)
        {

            if (isActive)
            {
                statusImage = Resources.Load<Sprite>($"Perk Upgrades Stuff/PerkOn");
                Perks.instance.AddPerk(onChoose.Invoke);
            }
            else
            {
                statusImage = Resources.Load<Sprite>($"Perk Upgrades Stuff/PerkOff");
                Perks.instance.RemovePerk(onChoose.Invoke);
            }


        }


        perkImage = Resources.Load<Sprite>($"Perk Upgrades Stuff/{perkName}");


        _transform = transform.GetChild(0).GetComponent<Transform>();
        _transform.GetChild(0).GetComponent<Image>().sprite = statusImage;
        _transform.GetChild(1).GetComponent<Image>().sprite = perkImage;
        perkDesc = _transform.GetChild(2).gameObject;
        perkDesc.GetComponent<TMP_Text>().text = I18n.Fields[perkDescription];
        priceText = _transform.GetChild(3).gameObject;
        _transform.GetChild(3).GetComponent<TMP_Text>().text = NewUpgradePrice.ToString();

        goldImage = _transform.GetChild(4).gameObject;

        if (chooseButton != null) chooseButton.onClick.AddListener(ChooseButton_Clicked);


    }

    private void Update()
    {
        NewUpgradePrice = SPrefs.GetInt($"perkUpgradePrice", basePrice);

        if (NewUpgradePrice >= basePrice)
        {
            _transform.GetChild(3).GetComponent<TMP_Text>().text = NewUpgradePrice.ToString();
        }

        if (!isLocked)
        {
            priceText.SetActive(false);
            goldImage.SetActive(false);
            perkDesc.transform.localPosition = new Vector3(perkDesc.transform.localPosition.x, 0f, perkDesc.transform.localPosition.z);
        }


    }


    private void ChooseButton_Clicked()
    {
        CheckPerkStatus();

    }

    private void CheckPerkStatus()
    {


        if (isLocked)
        {
            UpgradesController.instance.IsUnlockMenuActive = true;
            UpgradesController.instance.PerkName = perkName;
            UpgradesController.instance.PerkDescription = I18n.Fields[perkDescription];
            UpgradesController.instance.PerkImage = perkImage;
            UpgradesController.instance.PriceText = NewUpgradePrice.ToString();

            UpgradesController.instance.LastInstance = this;

            statusImage = Resources.Load<Sprite>($"Perk Upgrades Stuff/PerkLocked");
        }
        else
        {
            if (!isActive)
            {
                statusImage = Resources.Load<Sprite>($"Perk Upgrades Stuff/PerkOn");
                SPrefs.SetBool($"{perkName}_activated", true);
                SPrefs.Save();

                //Perks.instance.AddPerk(onChoose.Invoke);
                Perks.instance.ActivateUpgradePerks(perk);
                isActive = true;
            }
            else
            {
                statusImage = Resources.Load<Sprite>($"Perk Upgrades Stuff/PerkOff");
                SPrefs.SetBool($"{perkName}_activated", false);
                SPrefs.Save();
                //Perks.instance.RemovePerk(onChoose.Invoke);
                Perks.instance.RemoveUpgradePerks(perk);
                isActive = false;
            }

        }

        _transform.GetChild(0).GetComponent<Image>().sprite = statusImage;
    }

    public void UpdateStatus(bool locked)
    {
        isLocked = locked;
        CheckPerkStatus();

    }
}
