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

    public String HexadecimalCenterColor { get; private set; }

    /// <summary>
    /// 画像からカラーコードをとってくる関数
    /// とってくるのは画像のど真ん中の色
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetCenterColor(Texture2D date)
    {
        yield return new WaitForEndOfFrame();
        date.ReadPixels(new Rect(0, 0, date.width/2, date.height/2), 0, 0);

        centerColor = date.GetPixel(date.width / 2, date.height / 2);
        HexadecimalCenterColor = $"#{ColorUtility.ToHtmlStringRGB(centerColor)}";

        Debug.Log(HexadecimalCenterColor);
    }
}
