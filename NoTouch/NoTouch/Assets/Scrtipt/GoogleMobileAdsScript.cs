using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class GoogleMobileAdsScript : MonoBehaviour
{
    private RewardBasedVideoAd rewardBasedVideo;
#pragma warning disable 0649
    [SerializeField]
    private GameObject MoleWindow,PopWindow;
    [SerializeField]
    private GameObject SleepWindow,SleepWorkWindow;
#pragma warning restore 0649

    private string Test_UnitID = "ca-app-pub-3940256099942544/5224354917";
    public void Start()
    {

        rewardBasedVideo = RewardBasedVideoAd.Instance;


        RequestRewardBasedVideo();
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4699787160141871/3693930361"; //광고단위 ID
#elif UNITY_IPHONE
           string adUnitId = "ca-app-pub-4699787160141871/3693930361";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice("5919871F5E8EDA40").Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, Test_UnitID);
    }

    public void UserOptToWatchAd_Mole()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded_Mole;
        }
        RequestRewardBasedVideo();
    }

    public void UserOptToWatchAd_Item()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded_Item;

        }
        RequestRewardBasedVideo();
    }
    public void UserOptToWatchAd_Sleep()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded_Sleep;

        }
        RequestRewardBasedVideo();
    }
    public void HandleRewardBasedVideoRewarded_Mole(object sender, Reward args)
    {
        MoleController.Instance.AddMoney(3);
        PopWindow.SetActive(false);
        MoleWindow.SetActive(false);
    }
    public void HandleRewardBasedVideoRewarded_Sleep(object sender, Reward args)
    {
        CoworkerController.Instance.SleepJob(3);
        MineShopController.Instance.SleepWork(3);
        SleepWindow.SetActive(false);
        SleepWorkWindow.SetActive(true);
    }
    public void HandleRewardBasedVideoRewarded_Item(object sender, Reward args)
    {

        GameController.Instance.HaveItem[0]++;
        ItemUseController.Instance.ShowHaveItem();
    }
}

