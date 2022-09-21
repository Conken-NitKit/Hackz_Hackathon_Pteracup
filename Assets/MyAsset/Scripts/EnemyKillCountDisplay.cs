using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// EnemyKillCountTextの文字を管理するスクリプト
/// </summary>
public class EnemyKillCountDisplay : MonoBehaviour
{

    [SerializeField]
    private Text enemyKillCountText;

    [SerializeField]
    private float enemyKillCountTextPositionX = 98f;
    [SerializeField]
    private float enemyKillCountTextPositionY = 100f;
    [SerializeField]
    private float excutionTime = 2f;

    private float enemyKillCountTextPositionZ = 0f;

    /// <summary>
    /// enemyKillCountTextを動かす関数
    /// </summary>
    public void MoveText()
    {
        enemyKillCountText.transform.DOLocalMove(new Vector3(enemyKillCountTextPositionX, enemyKillCountTextPositionY, enemyKillCountTextPositionZ), excutionTime);
    }

}
