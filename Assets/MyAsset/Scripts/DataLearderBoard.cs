using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLearderBoard : MonoBehaviour
{
    public void UserLogin(string usename,int score)
    {
        PlayFabClientAPI.LoginWithCustomID
        (
            new LoginWithCustomIDRequest { CustomId = usename, CreateAccount = true},
            result =>
            {
                Debug.Log("ログイン成功！");
                SetPlayerDisplayName(usename,score);
            },
            error =>
            {
                Debug.Log("ログイン失敗");
            }
        );
    }
    public void SendStatisticUpdate (int score) 
    {
        // 送信したい更新情報
        PlayFabClientAPI.UpdatePlayerStatistics
        (
            new UpdatePlayerStatisticsRequest 
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate
                    {
                        StatisticName = "Scores",
                        Value = score
                    }
                }
            },
            result => {
                Debug.Log("Send score was succeeded.");
            },
            error => {
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }
    void SetPlayerDisplayName(string displayName,int score)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName
        (
            new UpdateUserTitleDisplayNameRequest 
            {
                DisplayName = displayName
            },
            result => 
            {
                Debug.Log("表示名決定！");
                SendStatisticUpdate (score);
            },
            error => 
            {
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }
}