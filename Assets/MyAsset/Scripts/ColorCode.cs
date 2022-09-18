using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCode : MonoBehaviour
{
    private Color centerColor;

    public Texture2D tex{ private get; set; }
    
    public String HexadecimalCenterColor { get; private set; }

    public IEnumerator GetCenterColor()
    {
        yield return new WaitForEndOfFrame();

        tex.ReadPixels(new Rect(0, 0, tex.width/2, tex.height/2), 0, 0);

        centerColor = tex.GetPixel(tex.width / 2, tex.height / 2);
        HexadecimalCenterColor = ColorUtility.ToHtmlStringRGB(centerColor);
        
        Debug.Log(HexadecimalCenterColor);
    }
}
