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
    CHEAT_DEATH,
    MOVE_HORIZONTAL,
    CHANCE_TO_HEAL_ON_HIT,
    DOUBLE_SHOT,
    AMMO_RECHARGE
}

public class Perks : MonoBehaviour
{
    [HideInInspector]public static Perks instance;


    private PerkList chosenPerk;
    private int selectedCharacter;

    private int ammoRewardThreshold = 6;
    private int ammoReward = 1;

    private bool rechargesAmmo = false;



    void Awake()
    {
        if(instance == null)
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

    }

    void Update()
    {

    }

    public void SetActivePerk(PerkList PERKS)
    {

        switch (PERKS)
        {

            case PerkList.REFUND_AMMO:
                SetAmmoRewardThreshold(3);
                SetAmmoReward(1);
                break;


            case PerkList.AMMO_RECHARGE:
                EnableAmmoRecharge();
                break;


            case PerkList.CHANCE_TO_PHASE:

                break;


            case PerkList.TAKE_LESS_DAMAGE:

                break;


            case PerkList.CHEAT_DEATH:

                break;


            case PerkList.MOVE_HORIZONTAL:

                break;


            case PerkList.CHANCE_TO_HEAL_ON_HIT:

                break;


            case PerkList.DOUBLE_SHOT:

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
}
