using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TestGameMgr : MonoBehaviour
{
    public ReactiveProperty<bool> isPlayerTurn = new ReactiveProperty<bool>(true);

    [SerializeField]
    private GameObject baseEnemy;

    [SerializeField]
    private BattleMonster monster;

    private GameObject enemyObject;
    public BattleEnemy enemy { get; private set; }
    
    private IDisposable subscription;

    private void Start()
    {
        GoNextStage();
    }

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
        
        Debug.Log("次の敵が現れた！");
        
        subscription = isPlayerTurn.Subscribe(x => {
            if (isPlayerTurn.Value)
            {
                Debug.Log("プレイヤーターン！");
            }
            else
            {
                Debug.Log("エネミーターン！");
                enemy.StartCoroutine(enemy.RunEnemyCommand());
            }
        });
    }
    
    public void DisplayStatus()
    {
        Debug.Log($"エネミーHP:{enemy.enemyNowHp.Value}");
        Debug.Log($"エネミーATK:{enemy.enemyNowAtk}");
        Debug.Log($"エネミーDef:{enemy.enemyNowDef}");
    }
}
