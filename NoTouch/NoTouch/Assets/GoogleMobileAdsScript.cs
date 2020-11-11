using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleMobileAdsScript : MonoBehaviour
{
    private RewardBasedVideoAd rewardBasedVideo;

    private string Test_UnitID = "	ca-app-pub-3940256099942544/5224354917";
    private string test_deviceID = "";
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
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, Test_UnitID);
    }

    private void UserOptToWatchAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            //TODO reward
        }
        RequestRewardBasedVideo();
    }
}

