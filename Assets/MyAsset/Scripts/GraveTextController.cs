using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// キャラクターの名前を取得し,表示するスクリプト
/// </summary>
public class GraveTextController : MonoBehaviour
{

    [SerializeField]
    private Text graveText;

    [SerializeField]
    private float excutionTime = 5f;

    private float alphaValue = 1f;

    private string characterName = "シャンクス";

    void Start()
    {
        graveText.text = $"{characterName}";
        graveText.DOFade(alphaValue, excutionTime);
    }

}
