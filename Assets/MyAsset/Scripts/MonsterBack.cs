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
        if (colorCode != null)
        {
            ColorUtility.TryParseHtmlString(colorCode.HexadecimalCenterColor, out color);
        }
        monsterBack.color = color;
    }
}
