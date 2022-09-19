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
    UnityEngine.UI.Text image;

    [SerializeField]
    private float excutionTime = 2;
    [SerializeField]
    private float positionX = 177;
    [SerializeField]
    private float positionY = 307;
    [SerializeField]
    private float transparency = 1;
    [SerializeField]
    private float waitTime = 2;

    private float positionZ;

    void Start()
    {
        this.transform.DOMove(new Vector3(positionX, positionY, positionZ), excutionTime);
        this.image.DOFade(transparency,excutionTime);
        positionY += 10;
        this.transform.DOMove(new Vector3(positionX, positionY, positionZ), excutionTime).SetDelay(waitTime).SetLoops(-1, LoopType.Yoyo);
    }
}
