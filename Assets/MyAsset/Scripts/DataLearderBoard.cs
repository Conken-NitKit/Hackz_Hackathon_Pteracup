using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataLearderBoard : MonoBehaviour
{
    private bool newAccount;　//アカウントを新規作成したかどうか
    private static readonly string ID_CHARACTERS = "0123456789";　//IDに使用する文字
    [SerializeField]
    private Text text;
    [SerializeField]
    private Text text2;
    [SerializeField]
    private Text text3;

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
    private void SendStatisticUpdate (int score) 
    {
        // 送信したい更新情報
        var statisticUpdate = new StatisticUpdate
        {
            StatisticName = "Scores",
            Value = score,
        };
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                statisticUpdate
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSuccess, OnError);
        void OnSuccess(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Send score was succeeded.");
        }
        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }
    private void SetPlayerDisplayName(string displayName,int score)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSuccess, OnError);
        void OnSuccess(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("表示名決定！");
            SendStatisticUpdate (score);
        }
        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }
    public void GetRanking()
    {
        PlayFabClientAPI.LoginWithCustomID
        (
            new LoginWithCustomIDRequest { CustomId = "Ranking", CreateAccount = true},
            result =>
            {
                Debug.Log("ログイン成功！");
            },
            error =>
            {
                Debug.Log("ログイン失敗");
            }
        );

        var request = new GetLeaderboardRequest
        {
            StatisticName = "Scores",
            StartPosition = 0, // 何位以降のランキングを取得するか指定します。
            MaxResultsCount = 3// ランキングデータを何件取得するか指定します。最大が100です。
        };
        PlayFabClientAPI.GetLeaderboard(request, OnSuccess, OnError);
        void OnSuccess(GetLeaderboardResult leaderboardResult)
        {
            // 実際は良い感じのランキングを表示するコードにします。
            foreach (var item in leaderboardResult.Leaderboard)
            {
                // Positionは順位です。0から始まるので+1して表示しています。
                Debug.Log("Success");
                text.text = string.Format($"{item.Position + 1}位: {item.DisplayName} - {item.StatValue}回");
            }
        }
        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }
}