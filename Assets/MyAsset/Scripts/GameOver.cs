using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GameOverScene����TitleScene�ɑJ�ڂ���X�N���v�g
/// </summary>
public class GameOver : MonoBehaviour
{
    private int score;
    private string charaName;

    [SerializeField] 
    private Text scoreText;

    [SerializeField]
    private SendRanking sendRanking;

    public async void Switch()
    {
        var sceneC = await SceneLoader.Load<Title>("Title");
    }
    
    public void SetArguments(int enemyKillScore,string characterName)
    {
        score = enemyKillScore;
        charaName = characterName;
        
        sendRanking.UserLogin(characterName,enemyKillScore);
        scoreText.text = $"敵を倒した数 : {score}体";
    }
}
