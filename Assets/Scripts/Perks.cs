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
        //EnableActivePerk();

        SetActivePerk(chosenPerk);
        
    }


    private Action EnabledPerk;

    public void AddPerk(Action action)
    {
        EnabledPerk -= action;
        EnabledPerk += action;
    }

    public void RemovePerk(Action action)
    {
        EnabledPerk -= action;
    }

    public void ConfigurePerk_AmmoReward()
    {
        SetAmmoRewardThreshold(3);
        SetAmmoReward(1);
    }

    public void ConfigurePerk_AmmoRecharge()
    {
        EnableAmmoRecharge();
    }

    public void ConfigurePerk_ChanceToPhase()
    {

    }

    public void ConfigurePerk_TakeLessDamage()
    {
        DamageReduction = 40; // %40 Percent Less Damage
    }

    public void ConfigurePerk_MoveHorizontal()
    {
        CanMoveHorizontally = true;
    }

    public void ConfigurePerk_ChanceToHeal()
    {
        ChanceToHeal = true;
    }

    public void ConfigurePerk_FasterBullets()
    {
        BulletSpeed = 2250f;
    }

    public void ConfigurePerk_LongerGracePeriod()
    {
        GracePeriod = 2.5f;
    }

    public void SetActivePerk(PerkList PERKS)
    {
        SetDefaultValues();


        switch (PERKS)
        {

            case PerkList.REFUND_AMMO:


                EnabledPerk = ConfigurePerk_AmmoReward;

                SetPerkDescription(I18n.Fields["T_GAIN_AMMO_MORE_FREQUENTLY"]);
                SetPerkImageName("FasterBullets");
                break;


            case PerkList.AMMO_RECHARGE:

                EnabledPerk = ConfigurePerk_AmmoRecharge;

                SetPerkDescription(I18n.Fields["T_AMMO_RECHARGES_OVER_TIME"]);
                SetPerkImageName("AmmoRecharge");
                break;


            case PerkList.CHANCE_TO_PHASE:

                EnabledPerk = ConfigurePerk_ChanceToPhase;

                SetPerkDescription(I18n.Fields["T_CHANCE_TO_PHASE"]);
                SetPerkImageName("Phasing");
                break;


            case PerkList.TAKE_LESS_DAMAGE:

                EnabledPerk = ConfigurePerk_TakeLessDamage;

                SetPerkDescription(I18n.Fields["T_TAKE_LESS_DAMAGE"]);
                SetPerkImageName("TakeLessDmg");
                break;


            case PerkList.MOVE_HORIZONTAL:

                EnabledPerk = ConfigurePerk_MoveHorizontal;

                SetPerkDescription(I18n.Fields["T_MOVE_HORIZONTALLY"]);
                SetPerkImageName("MoveHorizontal");
                break;


            case PerkList.CHANCE_TO_HEAL_ON_HIT:

                EnabledPerk = ConfigurePerk_ChanceToHeal;

                SetPerkDescription(I18n.Fields["T_CHANCE_TO_HEAL"]);
                SetPerkImageName("ChanceToHealOnKill");
                break;


            case PerkList.FASTER_BULLETS:


                EnabledPerk = ConfigurePerk_FasterBullets;

                SetPerkDescription(I18n.Fields["T_FASTER_BULLETS"]);
                SetPerkImageName("FasterBullets");
                break;

            case PerkList.LONGER_GRACE_PERIOD:


                EnabledPerk = ConfigurePerk_LongerGracePeriod;


                SetPerkDescription(I18n.Fields["T_LONGER_GRACE_PERIOD"]);
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
