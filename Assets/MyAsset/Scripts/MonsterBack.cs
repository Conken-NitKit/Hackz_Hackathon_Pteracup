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

    public void SetMonsterBackColor(string colorName = null)
    {
        ColorUtility.TryParseHtmlString(colorCode != null ? colorCode.HexadecimalCenterColor : colorName, out color);

        monsterBack.color = color;
    }
}
