using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TestGameMgr : MonoBehaviour
{
    public ReactiveProperty<bool> isPlayerTurn = new ReactiveProperty<bool>(true);
    [SerializeField]
    private TestEnemy enemy;

    private void Start()
    {
        IDisposable subscription = isPlayerTurn.Subscribe(x => {
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
}
