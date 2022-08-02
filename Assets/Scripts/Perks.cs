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

    private int healPercentage = 10;

    private bool rechargesAmmo = false;
    private bool canMoveHorizontally = false;
    private bool chanceToHealOnDestruction = false;

    private float fasterBulletValue;
    private float longerGracePeriodValue;
    private int dmgReductionPercent;

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

    public float BulletSpeed
    {
        get => fasterBulletValue;
        set => fasterBulletValue = value;
    }

    public float GracePeriod
    {
        get => longerGracePeriodValue;
        set => longerGracePeriodValue = value;
    }

    public int DamageReduction
    {
        get => dmgReductionPercent;
        set => dmgReductionPercent = value;
    }

    public bool CanMoveHorizontally
    {
        get => canMoveHorizontally;
        set => canMoveHorizontally = value;
    }

    public bool ChanceToHeal
    {
        get => chanceToHealOnDestruction;
        set => chanceToHealOnDestruction = value;
    }

    public float HealPercentage
    {
        get => healPercentage;
        private set { }
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



        string lastPerkStr = SPrefs.GetString("LastSelectedPerk", PerkList.DEFAULT_NONE.ToString());
        Enum.TryParse(lastPerkStr, out chosenPerk);
       

        // Don't need this in final release but for development purposes it can stay for now.
        SetActivePerk(chosenPerk);
        EnableActivePerk();


    }


    Action EnabledPerk;

    void Update()
    {

    }

    public void SetActivePerk(PerkList PERKS)
    {
        SetDefaultValues();


        switch (PERKS)
        {

            case PerkList.REFUND_AMMO:


                EnabledPerk = () =>
                {
                    SetAmmoRewardThreshold(3);
                    SetAmmoReward(1);
                };

                SetPerkDescription("Gains Ammo More Frequently");
                SetPerkImageName("FasterBullets");
                break;


            case PerkList.AMMO_RECHARGE:

                EnabledPerk = () =>
                {
                    EnableAmmoRecharge();
                };

                SetPerkDescription("Ammo Recharges Overtime");
                SetPerkImageName("AmmoRecharge");
                break;


            case PerkList.CHANCE_TO_PHASE:

                EnabledPerk = () =>
                {

                };

                SetPerkDescription("Chance To Phase Through Objects");
                SetPerkImageName("Phasing");
                break;


            case PerkList.TAKE_LESS_DAMAGE:

                EnabledPerk = () =>
                {
                    DamageReduction = 40; // %40 Percent Less Damage
                };

                SetPerkDescription("Takes Less Damage");
                SetPerkImageName("TakeLessDmg");
                break;


            case PerkList.MOVE_HORIZONTAL:

                EnabledPerk = () =>
                {
                    CanMoveHorizontally = true;
                };

                SetPerkDescription("Can Move Horizontally On Checkpoints");
                SetPerkImageName("MoveHorizontal");
                break;


            case PerkList.CHANCE_TO_HEAL_ON_HIT:

                EnabledPerk = () =>
                {
                    ChanceToHeal = true;
                };

                SetPerkDescription("Chance To Heal On Destruction");
                SetPerkImageName("ChanceToHealOnKill");
                break;


            case PerkList.FASTER_BULLETS:


                EnabledPerk = () =>
                {
                    BulletSpeed = 2250f;
                };

                SetPerkDescription("Your Bullets Travel Faster");
                SetPerkImageName("FasterBullets");
                break;

            case PerkList.LONGER_GRACE_PERIOD:



                EnabledPerk = () =>
                {
                    GracePeriod = 2.5f;
                };


                SetPerkDescription("Longer Grace Period After Getting Hit");
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

    public void EnableActivePerk()
    {

        EnabledPerk?.Invoke();
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

        BulletSpeed = 850f;
        GracePeriod = 1.25f;
        DamageReduction = 0;
        CanMoveHorizontally = false;
        ChanceToHeal = false;

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
