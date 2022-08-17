using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        var totalCurrency = SPrefs.GetInt("gameCurrency", 0);
        totalCurrencyText.text = totalCurrency.ToString();

        if (unlockButton != null) unlockButton.onClick.AddListener(() =>
        {
            IsUnlockMenuActive = false;
            SPrefs.SetBool($"{PerkName}_locked", false);

            LastInstance.UpdateStatus(false);

            Debug.Log($"Unlocked Upgrade! {PerkName}");
        });
    }

}
