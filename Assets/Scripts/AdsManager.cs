using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEditor;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private const string bannerId = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] private const string interstitialId = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] private const string rewardedId = "ca-app-pub-3940256099942544/5224354917";

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;



    private enum AD_Location
    {
        MainMenu,
        GameMenu
    }

    [SerializeField] private AD_Location adLocation = new AD_Location();


    private void Start()
    {

        //Debug.Log($"AD LOCATION IS NOW : {adLocation} STR {adLocation.ToString()}");

        MobileAds.Initialize(initStatus => { });

        switch (adLocation.ToString())
        {
            case "MainMenu":
                RequestBanner();
                break;
            case "GameMenu":
                if (bannerView != null) bannerView.Hide();
                RequestRewarded();
                break;
        }

        //if(MainMenu) RequestBanner();
        ////RequestInterstitial();
        //RequestRewarded();
    }




    // BANNER

    private void RequestBanner()
    {
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    // INTERSTITIAL

    private void RequestInterstitial()
    {
        interstitialAd = new InterstitialAd(interstitialId);

        interstitialAd.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    public void DisplayInterstitialAd()
    {
        if (interstitialAd.IsLoaded())
            interstitialAd.Show();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitialAd.Destroy();

        RequestInterstitial();
    }

    // REWARDED

    private void RequestRewarded()
    {
        rewardedAd = new RewardedAd(rewardedId);

        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        AdRequest request = new AdRequest.Builder().Build();

        rewardedAd.LoadAd(request);
    }

    public void DisplayRewardedAd()
    {
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        if(adLocation.ToString() == "GameMenu")
        {
            Debug.Log("REWARD FROM CONTINUE TOKEN");
            FindObjectOfType<LevelManager>().onContinue();
        }
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RequestRewarded();
    }

    private void OnDestroy()
    {

        if (adLocation.ToString() == "GameMenu")
        {
            rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
            rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
        }

    }
}