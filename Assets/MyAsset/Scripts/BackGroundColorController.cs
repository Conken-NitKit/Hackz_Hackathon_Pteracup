using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TitleSceneの背景とボタン、タイトルの色を変更するスクリプト
/// </summary>
public class BackGroundColorController : MonoBehaviour
{

    private float redValue = 0;
    private float greenValue = 0;
    private float blueValue = 0;
    private float colorLimit = 255;

    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text startText;
    [SerializeField]
    private Text imageSelectionText;
    [SerializeField]
    private Text rankingText;

    void Start()
    {
        
    }

    void FixedUpdate()
    {

        redValue += Mathf.Sin(Time.deltaTime) * colorLimit;
        greenValue += Mathf.Cos(Time.deltaTime) * colorLimit;
        blueValue += (Mathf.Sin(Time.deltaTime) + Mathf.Cos(Time.deltaTime)) * colorLimit;


        redValue %= colorLimit;
        greenValue %= colorLimit;
        blueValue %= colorLimit;

        startText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
        imageSelectionText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
        rankingText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
        titleText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
        
    }
}
