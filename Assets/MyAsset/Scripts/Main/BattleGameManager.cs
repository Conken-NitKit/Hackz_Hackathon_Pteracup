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

    [SerializeField] 
    private GameObject buttonBlockPanel;

    [SerializeField]
    private SceneAnimation sceneAnimation;

    public int enemyKillNum{ get; private set; }
    
    /// <summary>
    /// 敵が倒れた時、次のバトルの準備をするメソッド
    /// </summary>
    public void GoNextStage()
    {
        if (!(subscription == null))
        {
            subscription.Dispose();
        }
        
        enemyObject = Instantiate(baseEnemy,new Vector3( -10f, 1.6f, 0.0f), Quaternion.identity);
        enemy = enemyObject.GetComponent<BattleEnemy>();

        enemyKillNum++;
        
        gameText.text = "次の敵が現れた！";
        
        subscription = isPlayerTurn.Subscribe(x => {
            if (isPlayerTurn.Value)
            {
                gameText.text = "プレイヤーターン！";
                buttonBlockPanel.SetActive(false);
            }
            else
            {
                if (sceneAnimation.goNextStage)
                {
                    isPlayerTurn.Value = true;
                }
                else
                {
                    gameText.text = "エネミーターン！";
                    enemy.StartCoroutine(enemy.RunEnemyCommand());
                }
            }
        });
        
        enemy.BuildEnemyStats();
        
        monster.TargetEnemy();
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