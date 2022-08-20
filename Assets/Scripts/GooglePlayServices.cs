using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;
using TMPro;

public class GooglePlayServices : MonoBehaviour
{
    public static GooglePlayServices instance;
    private bool connectedToGooglePlay;

   [SerializeField] private TMP_Text debugtext;



    public bool GooglePlayConnection
    {
        get => connectedToGooglePlay;
        set => connectedToGooglePlay = value;
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
        Authenticate();
    }

    public void Authenticate()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {

        debugtext.text = $"Authenticating...";

        if (status == SignInStatus.Success)
        {
            GooglePlayConnection = true;

            debugtext.text = "Successfully Authenticated!\n" +
                             $"Hello {Social.localUser.userName}"; //  ID : {Social.localUser.id}
        }
        else
        {
            GooglePlayConnection = false;

            debugtext.text = status.ToString();
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }

    }


    public void CheckAchievementStatus(string ID, Action<bool> cb)
    {

        Social.LoadAchievements(achievements => {
            if (achievements.Length > 0)
            {
                Debug.Log("Got " + achievements.Length + " achievement instances");
                foreach (IAchievement achievement in achievements)
                {

                    if (achievement.id == ID)
                    {
                        if (achievement.completed)
                        {
                            cb(true);
                        }
                        else
                        {
                            cb(false);
                        }
                    }

                }
            }
        });
    }

}
