using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class GoogleHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        .RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        .RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        .RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        
        // authenticate user:
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            // handle results
            Debug.Log("Google login: " + result);
        });
    }
    public void unlockachievement(string id)//내부업적달성후 호출하여 구글플레이에 전달
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>{
            // handle success or failure
        });
    }    // 
    public void ReportLeaderboad(string id, long currentValue)
    {
        Social.ReportScore(currentValue, id, (bool success) =>
        {
            // handle success or failure
        });
    }
    public void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }

}
