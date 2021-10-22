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
    [SerializeField]
    private GameObject GetDaziWindow;
    [SerializeField]
    private Text GetDaziText;
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
        //AdRequest request = new AdRequest.Builder().AddTestDevice("5919871F5E8EDA40").Build();
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, adUnitId);
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
    public void UserOptToWatchAd_RandomItem()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded_RandomItem;

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
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded_Mole;
    }
    public void HandleRewardBasedVideoRewarded_Sleep(object sender, Reward args)
    {
        CoworkerController.Instance.SleepJob(3);
        MineShopController.Instance.SleepWork(3);
        SleepWindow.SetActive(false);
        SleepWorkWindow.SetActive(true);
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded_Sleep;
    }
    public void HandleRewardBasedVideoRewarded_Item(object sender, Reward args)
    {

        GameController.Instance.HaveItem[0]++;
        GetDaziText.text = "실버다지 1개를 획득했습니다.";
        GetDaziWindow.gameObject.SetActive(true);
        ItemUseController.Instance.ShowHaveItem();
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded_Item;
    }
    public void HandleRewardBasedVideoRewarded_RandomItem(object sender, Reward args)
    {
        string DaziName;
        int DaziAmount;
        int num = UnityEngine.Random.Range(0, 100);

        if (num < 1)
        {
            GameController.Instance.HaveItem[1] += 3;
            DaziName = "골드다지";
            DaziAmount = 3;
        }
        else if(num<4)
        {
            GameController.Instance.HaveItem[1] += 2;
            DaziName = "골드다지";
            DaziAmount = 2;
        }
        else if(num<9)
        {
            GameController.Instance.HaveItem[1] += 1;
            DaziName = "골드다지";
            DaziAmount = 1;
        }
        else if(num<15)
        {
            GameController.Instance.HaveItem[0] += 6;
            DaziName = "실버다지";
            DaziAmount = 6;
        }
        else if(num<25)
        {
            GameController.Instance.HaveItem[0] += 5;
            DaziName = "실버다지";
            DaziAmount = 5;
        }
        else if(num<40)
        {
            GameController.Instance.HaveItem[0] += 4;
            DaziName = "실버다지";
            DaziAmount = 4;
        }
        else if(num<60)
        {
            GameController.Instance.HaveItem[0] += 3;
            DaziName = "실버다지";
            DaziAmount = 3;
        }
        else if(num<90)
        {
            GameController.Instance.HaveItem[0] += 2;
            DaziName = "실버다지";
            DaziAmount = 2;
        }
        else
        {
            GameController.Instance.HaveItem[0] += 1;
            DaziName = "실버다지";
            DaziAmount = 1;
        }
        GetDaziText.text = string.Format("{0} {1}개를 획득했습니다.",DaziName,DaziAmount);
        GetDaziWindow.gameObject.SetActive(true);
        ItemUseController.Instance.ShowHaveItem();
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded_RandomItem;
    }
}

