using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneAnimation : MonoBehaviour
{
    [SerializeField] 
    private GameObject curtains;
    
    public bool goNextStage { get; set; }

    private void Start()
    {
        RiseCurtain();
    }

    public void RiseCurtain()
    {
        curtains.transform.DOMoveY(15f,6f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                curtains.transform.position = new Vector3(0, -15f, 0);
                goNextStage = false;
            });
    }
}
