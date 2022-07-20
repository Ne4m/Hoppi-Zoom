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

    public static AdsManager instance;

    private float updateInterval = 2f;
    private float lastUpdateCheck;

    private int interstitialTriggerCounter = 0;

    //private enum AD_Location
    //{
    //    MainMenu,
    //    GameMenu
    //}

    //[SerializeField] private AD_Location adLocation = new AD_Location();

    private bool isBannerActive = false;

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

        //switch (adLocation.ToString())
        //{
        //    case "MainMenu":
        //        RequestBanner();
        //        break;
        //    case "GameMenu":
        //        if (bannerView != null) bannerView.Destroy();
        //        RequestRewarded();
        //        break;
        //}

        //RequestBanner();
       // RequestInterstitial();
        //RequestRewarded();


        //if(MainMenu) RequestBanner();
        ////RequestInterstitial();
        //RequestRewarded();
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

                    Debug.Log("Requested Banner!");
                }



            }
            else if (bannerView != null)
            {
                bannerView.Destroy();
                isBannerActive = false;
            }

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {

                RequestRewarded();
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

    private void RequestRewarded()
    {
        Debug.Log("Requesting Rewarded...");

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


        if (bannerView != null) bannerView.Destroy();

    }
}