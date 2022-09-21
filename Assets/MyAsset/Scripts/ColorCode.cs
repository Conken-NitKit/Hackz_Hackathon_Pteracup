using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画像のカラーコード決定クラス
/// </summary>
public class ColorCode : MonoBehaviour
{
    private Color centerColor;

    public Texture2D Tex{ private get; set; }
    
    public String HexadecimalCenterColor { get; private set; }

    [SerializeField]
    private MonsterBack monsterBack;

    /// <summary>
    /// 画像からカラーコードをとってくる関数
    /// とってくるのは画像のど真ん中の色
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetCenterColor()
    {
        yield return new WaitForEndOfFrame();

        Tex.ReadPixels(new Rect(0, 0, Tex.width/2, Tex.height/2), 0, 0);

        centerColor = Tex.GetPixel(Tex.width / 2, Tex.height / 2);
        HexadecimalCenterColor = $"#{ColorUtility.ToHtmlStringRGB(centerColor)}";

        monsterBack.SetMonsterBackColor();
        
        Debug.Log(HexadecimalCenterColor);
    }
}
