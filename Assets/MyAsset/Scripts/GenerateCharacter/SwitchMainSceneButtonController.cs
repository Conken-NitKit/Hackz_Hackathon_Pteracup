using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchMainSceneButtonController : MonoBehaviour
{

    [SerializeField]
    private float positionX;
    [SerializeField]
    private float positionY;
    [SerializeField]
    private float excutionTime;
    [SerializeField]
    private float moveDestance;

    private float positionZ = 0;
    private float zero = 0;
    
    public void FadeIn()
    {
        this.transform.DOLocalMove(new Vector3(positionX, positionY, positionZ), excutionTime);
    }

    public void FadeOut()
    {
        this.transform.DOLocalMove(new Vector3(zero, moveDestance, zero), excutionTime).SetRelative(true);
    }

}
