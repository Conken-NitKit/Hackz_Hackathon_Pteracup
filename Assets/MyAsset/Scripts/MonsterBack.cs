using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBack : MonoBehaviour
{
    [SerializeField] 
    private ColorCode colorCode;

    private Color color;

    [SerializeField]
    private Image monsterBack;

    public void SetMonsterBackColor()
    {
        Debug.Log(colorCode.HexadecimalCenterColor);
        ColorUtility.TryParseHtmlString(colorCode.HexadecimalCenterColor, out color);
        Debug.Log(color);
        monsterBack.color = color;
    }
}
