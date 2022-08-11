using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;


public class AdsManager : MonoBehaviour
{
    [SerializeField] private const string bannerId = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] private const string interstitialId = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] private const string rewardedId = "ca-app-pub-3940256099942544/5224354917";

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedAd rewardedAd_DoubleEarn;
    private RewardedAd rewardedAd_SkinUnlock;
    private RewardedAd rewardedAd_CharacterUnlock;

    public static AdsManager instance;

    private float updateInterval = 2f;
    private float lastUpdateCheck;

    private int interstitialTriggerCounter = 0;

    private string mainMenuActiveUI;


    public string MainMenuActiveUI
    {
        get => mainMenuActiveUI;
        set => mainMenuActiveUI = value;
    }

    //private enum AD_Location
    //{
    //    MainMenu,
    //    GameMenu
    //}

    //[SerializeField] private AD_Location adLocation = new AD_Location();

    private bool isBannerActive = false;

    public bool BannerActive
    {
        get => isBannerActive;
        set => isBannerActive = value;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        //Debug.Log($"AD LOCATION IS NOW : {adLocation} STR {adLocation.ToString()}");

        MobileAds.Initialize(initStatus => { 
        
        });

        RequestRewarded_CharacterUnlock();
        RequestRewarded_SkinUnlock();
    }

    private void Update()
    {
        WaitForSeconds(updateInterval, () =>
        {


            if (SceneManager.GetActiveScene().buildIndex == 0)
            {

                if (!isBannerActive)
                {
                    RequestBanner();

                    //Debug.Log("Requested Banner!");
                }

                //if(MainMenuActiveUI == "character")
                //{
                //    RequestRewarded_CharacterUnlock();
                //}
                //else if(MainMenuActiveUI == "accessory")
                //{
                //    RequestRewarded_SkinUnlock();
                //}

            }
            else if (bannerView != null)
            {
                bannerView.Destroy();
                isBannerActive = false;
            }

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {

                if(rewardedAd == null) RequestRewarded();
                if(rewardedAd_DoubleEarn == null) RequestRewarded_DoubleEarn();
                RequestInterstitial();
            }

        });

        if (GetInterstitialTriggerCounter() > 2)
        {
            DisplayInterstitialAd();
        }
    }


    private void WaitForSeconds(float interval, Action action)
    {

        if(Time.time > interval + lastUpdateCheck)
        {

            action();

            lastUpdateCheck = Time.time;
        } 
    }

    // BANNER

    private void RequestBanner()
    {
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        isBannerActive = true;

    }

    // INTERSTITIAL

    private void RequestInterstitial()
    {
        Debug.Log("Requesting Interstitial...");

        interstitialAd = new InterstitialAd(interstitialId);

        interstitialAd.OnAdClosed += HandleOnAdClosed;

        AdRequest requestInterstitial = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(requestInterstitial);

       // if (interstitialAd.IsLoaded()) Debug.Log("Interstitial Ad Is Loaded!");
    } 

    public void DisplayInterstitialAd()
    {

        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();

            SetInterstitialTriggerCounter(0);
        }

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitialAd.Destroy();
        RequestInterstitial();
    }

    // REWARDED

    #region END SCREEN CONTINUE
    private void RequestRewarded()
    {
        //Debug.Log("Requesting Rewarded...");

        rewardedAd = new RewardedAd(rewardedId);

        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        AdRequest requestRewarded = new AdRequest.Builder().Build();

        rewardedAd.LoadAd(requestRewarded);

       // if (rewardedAd.IsLoaded()) Debug.Log("Rewarded Ad Is Loaded!");
    }

    public void DisplayRewardedAd()
    {

        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }

    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        Debug.Log("REWARD FROM CONTINUE TOKEN");
        FindObjectOfType<LevelManager>().onContinue();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        rewardedAd.Destroy();
        RequestRewarded();
    }
    #endregion

    #region END SCREEN DOUBLE EARNING
    private void RequestRewarded_DoubleEarn()
    {
        //Debug.Log("Requesting Rewarded...");

        rewardedAd_DoubleEarn = new RewardedAd(rewardedId);

        rewardedAd_DoubleEarn.OnAdClosed += HandleRewardedAdClosed_DoubleEarn;
        rewardedAd_DoubleEarn.OnUserEarnedReward += HandleUserEarnedReward_DoubleEarn;
        rewardedAd_DoubleEarn.OnAdFailedToLoad += HandleUserFailedToLoad_DoubleEarn;

        AdRequest requestRewarded = new AdRequest.Builder().Build();

        rewardedAd_DoubleEarn.LoadAd(requestRewarded);

        // if (rewardedAd.IsLoaded()) Debug.Log("Rewarded Ad Is Loaded!");
    }

    public void DisplayRewardedAd_DoubleEarn()
    {

        if (rewardedAd_DoubleEarn.IsLoaded())
        {
            rewardedAd_DoubleEarn.Show();
        }

    }

    public void HandleUserEarnedReward_DoubleEarn(object sender, EventArgs args)
    {
        Debug.Log("REWARD FROM DOUBLING POINTS");
        FindObjectOfType<LevelManager>().DoubleEarnedCurrency();
    }

    public void HandleRewardedAdClosed_DoubleEarn(object sender, EventArgs args)
    {
        rewardedAd_DoubleEarn.Destroy();
        RequestRewarded_DoubleEarn();
    }

    public void HandleUserFailedToLoad_DoubleEarn(object sender, EventArgs args)
    {
        FindObjectOfType<UI_Animations>().EarningButtonReward_Error();
    }
    #endregion


    // CHARRACTER UNLOCK 
    #region CHARACTER UNLOCK
    private void RequestRewarded_CharacterUnlock()
    {
       // Debug.Log("Requesting Rewarded...");

        if(rewardedAd_CharacterUnlock != null) rewardedAd_CharacterUnlock.Destroy();

        rewardedAd_CharacterUnlock = new RewardedAd(rewardedId);

        rewardedAd_CharacterUnlock.OnAdClosed += HandleRewardedAdClosed_CharacterUnlock;
        rewardedAd_CharacterUnlock.OnUserEarnedReward += HandleUserEarnedReward_CharacterUnlock;
        rewardedAd_CharacterUnlock.OnAdFailedToShow += HandleRewardedAdFailedToShow_CharacterUnlock;

        AdRequest requestRewarded = new AdRequest.Builder().Build();

        rewardedAd_CharacterUnlock.LoadAd(requestRewarded);

        // if (rewardedAd.IsLoaded()) Debug.Log("Rewarded Ad Is Loaded!");
    }

    public void DisplayRewardedAd_CharacterUnlock()
    {
        if (rewardedAd_CharacterUnlock.IsLoaded())
        {
            rewardedAd_CharacterUnlock.Show();
        }

    }

    public void HandleUserEarnedReward_CharacterUnlock(object sender, EventArgs args)
    {
        Debug.Log("REWARD FROM CHARACTER UNLOCK");
        FindObjectOfType<CharacterMenuController>().ApplyAdDiscount();
    }

    public void HandleRewardedAdClosed_CharacterUnlock(object sender, EventArgs args)
    {
        
        RequestRewarded_CharacterUnlock();
    }

    public void HandleRewardedAdFailedToShow_CharacterUnlock(object sender, EventArgs args)
    {
        FindObjectOfType<CharacterMenuController>().ApplyAdDiscount_Error();
    }
    #endregion

    #region SKIN UNLOCK
    private void RequestRewarded_SkinUnlock()
    {
        //Debug.Log("Requesting Rewarded...");

        if (rewardedAd_SkinUnlock != null) rewardedAd_SkinUnlock.Destroy();

        rewardedAd_SkinUnlock = new RewardedAd(rewardedId);

        rewardedAd_SkinUnlock.OnAdClosed += HandleRewardedAdClosed_SkinUnlock;
        rewardedAd_SkinUnlock.OnUserEarnedReward += HandleUserEarnedReward_SkinUnlock;
        rewardedAd_SkinUnlock.OnAdFailedToShow += HandleRewardedAdFailedToShow_SkinUnlock;

        AdRequest requestRewarded = new AdRequest.Builder().Build();

        rewardedAd_SkinUnlock.LoadAd(requestRewarded);

        // if (rewardedAd_SkinUnlock.IsLoaded()) Debug.Log("Rewarded Ad Is Loaded!");
    }

    public void DisplayRewardedAd_SkinUnlock()
    {

        if (rewardedAd_SkinUnlock.IsLoaded())
        {
            rewardedAd_SkinUnlock.Show();
        }

    }

    public void HandleUserEarnedReward_SkinUnlock(object sender, EventArgs args)
    {
        Debug.Log("REWARD FROM SKIN MANAGER UNLOCK");
        SkinManager.instance.ApplySkinAdDiscount();
    }

    public void HandleRewardedAdClosed_SkinUnlock(object sender, EventArgs args)
    {
        //rewardedAd_SkinUnlock.Destroy();
        RequestRewarded_SkinUnlock();
    }

    public void HandleRewardedAdFailedToShow_SkinUnlock(object sender, EventArgs args)
    {
        SkinManager.instance.ApplySkinAdDiscount_Error();
    }
    #endregion




    public int GetInterstitialTriggerCounter()
    {
        return interstitialTriggerCounter;
    }
    
    public void SetInterstitialTriggerCounter(int val)
    {
        interstitialTriggerCounter = val;
    }

    public void IncreaseInterstitialTriggerCounter(int amount)
    {
        SetInterstitialTriggerCounter(GetInterstitialTriggerCounter() + amount);
    }

    private void OnDestroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
            rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
        }

        if (rewardedAd_CharacterUnlock != null)
        {
            rewardedAd_CharacterUnlock.OnUserEarnedReward -= HandleUserEarnedReward_CharacterUnlock;
            rewardedAd_CharacterUnlock.OnAdClosed -= HandleRewardedAdClosed_CharacterUnlock;
        }

        if (rewardedAd_DoubleEarn != null)
        {
            rewardedAd_DoubleEarn.OnUserEarnedReward -= HandleUserEarnedReward_DoubleEarn;
            rewardedAd_DoubleEarn.OnAdClosed -= HandleRewardedAdClosed_DoubleEarn;
        }

        if(rewardedAd_SkinUnlock != null)
        {
            rewardedAd_SkinUnlock.OnUserEarnedReward -= HandleUserEarnedReward_SkinUnlock;
            rewardedAd_SkinUnlock.OnAdClosed -= HandleRewardedAdClosed_SkinUnlock;
        }


        if (bannerView != null) bannerView.Destroy();

    }
}