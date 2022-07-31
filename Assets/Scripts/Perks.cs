using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum PerkList
{
    DEFAULT_NONE,
    MORE_AMMO,
    REFUND_AMMO,
    CHANCE_TO_PHASE,
    TAKE_LESS_DAMAGE,
    MOVE_HORIZONTAL,
    CHANCE_TO_HEAL_ON_HIT,
    FASTER_BULLETS,
    LONGER_GRACE_PERIOD,
    AMMO_RECHARGE
}

public class Perks : MonoBehaviour
{
    [HideInInspector] public static Perks instance;


    private PerkList chosenPerk;
    private int selectedCharacter;

    private int ammoRewardThreshold = 6;
    private int ammoReward = 1;

    private bool rechargesAmmo = false;

    private string lastPerkDescription, lastPerkImageName;

    public string LastPerkDescription
    {
        get => lastPerkDescription;
        set => lastPerkDescription = value;
    }

    public string LastPerkImage
    {
        get => lastPerkImageName;
        set => lastPerkImageName = value;
    }



    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {


        //foreach (var PERK in Enum.GetValues(typeof(PerkList)))
        //{
        //}
        string lastPerkStr = SPrefs.GetString("LastSelectedPerk", PerkList.DEFAULT_NONE.ToString());
        Enum.TryParse(lastPerkStr, out chosenPerk);
       // Debug.Log($"CHOSEN PERK BY HAND IS {lastPerkStr} & {chosenPerk}");

        // Don't need this in final release but for development purposes it can stay for now.
        SetActivePerk(chosenPerk);

        //characterMenuController.SetPerksDescription("TEST 123");
    }

    void Update()
    {

    }

    public void SetActivePerk(PerkList PERKS)
    {
        SetDefaultValues();


        switch (PERKS)
        {

            case PerkList.REFUND_AMMO:
                SetAmmoRewardThreshold(3);
                SetAmmoReward(1);


                SetPerkDescription("Gains Ammo More Frequently");
                SetPerkImageName("FasterBullets");
                break;


            case PerkList.AMMO_RECHARGE:
                EnableAmmoRecharge();

                SetPerkDescription("Ammo Recharges Overtime");
                SetPerkImageName("AmmoRecharge");
                break;


            case PerkList.CHANCE_TO_PHASE:


                SetPerkDescription("Chance To Phase Through Objects");
                SetPerkImageName("Phasing");
                break;


            case PerkList.TAKE_LESS_DAMAGE:


                SetPerkDescription("Takes Less Damage");
                SetPerkImageName("TakeLessDmg");
                break;


            case PerkList.MOVE_HORIZONTAL:


                SetPerkDescription("Can Move Horizontally On Checkpoints");
                SetPerkImageName("MoveHorizontal");
                break;


            case PerkList.CHANCE_TO_HEAL_ON_HIT:


                SetPerkDescription("Chance To Heal On Kill");
                SetPerkImageName("ChanceToHealOnKill");
                break;


            case PerkList.FASTER_BULLETS:

                SetPerkDescription("Your Bullets Travel Faster");
                SetPerkImageName("FasterBullets");
                break;

            case PerkList.LONGER_GRACE_PERIOD:


                SetPerkDescription("Longer Grace Period");
                SetPerkImageName("LongerGracePeriod");
                break;

            case PerkList.DEFAULT_NONE:
                SetDefaultValues();
                break;

            default:
                SetDefaultValues();
                break;
        }
    }

    #region REFUND_AMMO PERK
    public void SetAmmoRewardThreshold(int val)
    {
        ammoRewardThreshold = val;
    }

    public void SetAmmoReward(int val)
    {
        ammoReward = val;
    }

    public int GetAmmoRewardThreshold()
    {
        return ammoRewardThreshold;
    }

    public int GetAmmoReward()
    {
        return ammoReward;
    }
    #endregion

    #region RECHARGE_AMMO_PERK

    public void EnableAmmoRecharge()
    {
        rechargesAmmo = true;
    }

    public void DisableAmmoRecharge()
    {
        rechargesAmmo = false;
    }

    public bool CanRechargeAmmo()
    {
        return rechargesAmmo;
    }

    #endregion

    public void SetDefaultValues()
    {
        SetPerkDescription("none");
        SetPerkImageName("none");


        SetAmmoRewardThreshold(6);
        SetAmmoReward(1);
        DisableAmmoRecharge();
    }
    public void SetSelectedCharacterPerk(PerkList perk)
    {
        this.chosenPerk = perk;
        SetActivePerk(chosenPerk);
        Debug.Log($"Chosen Perk: {chosenPerk}");

    }


    private void SetPerkDescription(string value)
    {
        // SPrefs.SetString("LastPerkDescription", value);
        LastPerkDescription = value;

    }

    private void SetPerkImageName(string value)
    {
        // SPrefs.SetString("LastPerkImageName", value);

        LastPerkImage = value;
    }

}
