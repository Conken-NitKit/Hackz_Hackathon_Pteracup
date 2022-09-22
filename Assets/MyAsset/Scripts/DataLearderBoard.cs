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
    private Text text = default;

    public void Start()
    {
        GetRanking();
    }
    
    public void GetRanking()
    {
        PlayFabClientAPI.LoginWithCustomID
        (
            new LoginWithCustomIDRequest { CustomId = "Ranking", CreateAccount = true},
            result =>
            {
                Debug.Log("ログイン成功！");
                GetLeaderboard();
            },
            error =>
            {
                Debug.Log("ログイン失敗");
            }
        );
        void UpdateUserName() 
        {
            //ユーザ名を指定して、UpdateUserTitleDisplayNameRequestのインスタンスを生成
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = text.text,
            };
            //ユーザ名の更新
            Debug.Log($"ユーザ名の更新開始");
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserNameSuccess, OnUpdateUserNameFailure);
        }
        //ユーザ名の更新成功
        void OnUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result){
            //result.DisplayNameに更新した後のユーザ名が入ってる
            Debug.Log($"ユーザ名の更新が成功しました : {result.DisplayName}");
        }
        //ユーザ名の更新失敗
        void OnUpdateUserNameFailure(PlayFabError error){
            Debug.LogError($"ユーザ名の更新に失敗しました\n{error.GenerateErrorReport()}");
        }
    
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Scores",
            StartPosition = 0, // 何位以降のランキングを取得するか指定します。
            MaxResultsCount = 3// ランキングデータを何件取得するか指定します。最大が100です。
        };
        void GetLeaderboard()
        {
            PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
            {
                StatisticName = "Scores"
            }, 
            result =>
            {
                foreach (var item in result.Leaderboard)
                {
                    text.text += string.Format($"{item.Position + 1}位:{item.DisplayName} " + $"スコア {item.StatValue}\n");
                }
            }, 
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });
        }
    }
}