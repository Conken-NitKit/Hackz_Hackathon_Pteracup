using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// GameOverTextの動きを管理するスクリプト
/// </summary>
public class GameOverTextController : MonoBehaviour
{

    [SerializeField]
    private EnemyKillCountDisplay enemyKillCountDisplay;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private float excutionTime = 5f;

    private float alphaValue = 1f;
    private float positionX = 0f;
    private float positionY = 0f;
    private float positionZ = 0f;

    void Start()
    {
        StartCoroutine(MoveText());
    }

    /// <summary>
    /// GameOverTextのalpha値,positionを移動させるコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveText()
    {
        gameOverText.DOFade(alphaValue, excutionTime);
        gameOverText.transform.DOLocalMove(new Vector3(positionX, positionY, positionZ), excutionTime);
        yield return new WaitForSeconds(excutionTime);
        enemyKillCountDisplay.MoveText();
    }
}
