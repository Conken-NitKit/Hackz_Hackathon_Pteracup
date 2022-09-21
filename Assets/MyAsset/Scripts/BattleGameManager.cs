using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// 戦闘時のゲーム管理クラス
/// </summary>
public class BattleGameManager : MonoBehaviour
{
    public ReactiveProperty<bool> isPlayerTurn = new ReactiveProperty<bool>(true);

    [SerializeField]
    private GameObject baseEnemy;

    [SerializeField]
    private BattleMonster monster;

    private GameObject enemyObject;
    public BattleEnemy enemy { get; private set; }
    
    private IDisposable subscription;

    [SerializeField]
    private Text gameText;

    private int enemyKillNum;

    private void Start()
    {
        GoNextStage();
    }

    /// <summary>
    /// 敵が倒れた時、次のバトルの準備をするメソッド
    /// </summary>
    public void GoNextStage()
    {
        if (!(subscription == null))
        {
            subscription.Dispose();
        }
        
        enemyObject = Instantiate(baseEnemy,new Vector3( -1.0f, 0.0f, 0.0f), Quaternion.identity);
        enemy = enemyObject.GetComponent<BattleEnemy>();
        enemy.BuildEnemyStats();
        
        monster.TargetEnemy();

        enemyKillNum++;
        
        gameText.text = "次の敵が現れた！";
        
        subscription = isPlayerTurn.Subscribe(x => {
            if (isPlayerTurn.Value)
            {
                gameText.text = "プレイヤーターン！";
            }
            else
            {
                gameText.text = "エネミーターン！";
                enemy.StartCoroutine(enemy.RunEnemyCommand());
            }
        });
    }
    
    /// <summary>
    /// エネミーのステータスを見れるテストクラス
    /// </summary>
    public void DisplayStatus()
    {
        Debug.Log($"エネミーHP:{enemy.enemyNowHp.Value}");
        Debug.Log($"エネミーATK:{enemy.enemyNowAtk}");
        Debug.Log($"エネミーDef:{enemy.enemyNowDef}");
    }
}