using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// titleのアニメーションを制御するスクリプト
/// </summary>
public class TitleController : MonoBehaviour
{

    [SerializeField]
    UnityEngine.UI.Text titleText;

    [SerializeField]
    private float excutionTime = 2;
    [SerializeField]
    private float transparency = 1;
    [SerializeField]
    private float moveDestance = 10;

    private float zero = 0;

    void Start()
    {
        //this.transform.DOLocalMove(new Vector3(positionX, positionY, positionZ), excutionTime);
        this.titleText.DOFade(transparency,excutionTime);
        this.transform.DOLocalMove(new Vector3(zero, moveDestance, zero), excutionTime).SetLoops(-1, LoopType.Yoyo).SetRelative(true);
    }
}
