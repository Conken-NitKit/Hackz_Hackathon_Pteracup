using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Iei��IeiFlame�𑀂�X�N���v�g
/// </summary>
public class IeiController : MonoBehaviour
{
    [SerializeField]
    private Image IeiFlameImage;
    [SerializeField]
    private Image IeiImage;

    [SerializeField]
    private float excutionTime = 5f;

    private float alphaValue = 1f;

    void Start()
    {
        IeiImage.DOFade(alphaValue, excutionTime);
        IeiFlameImage.DOFade(alphaValue, excutionTime);
    }
}
