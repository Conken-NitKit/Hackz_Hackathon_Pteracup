using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// textの色を変化させるスクリプト
/// </summary>
public class TextColorController : MonoBehaviour
{


    private float redValue = 0;
    private float greenValue = 0;
    private float blueValue = 0;
    private float colorLimit = 255;
    private float harf = 2;
    private float quarter = 4;
    private float excutionTime = 2;

    private int randomValue;

    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text startText;
    [SerializeField]
    private Text imageSelectionText;
    [SerializeField]
    private Text rankingText;

    [SerializeField]
    private Color[] colorList;

    void Start()
    {
        redValue = Random.Range(0, 255);
        greenValue = Random.Range(0, 255);
        blueValue = Random.Range(0, 255);
        /*
        startText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, (byte)colorLimit);
        imageSelectionText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, (byte)colorLimit);
        */
        titleText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, (byte)colorLimit);
        StartCoroutine("SwitchColor");
    }

    void FixedUpdate()
    {
        redValue += Mathf.Sin(Time.deltaTime) * colorLimit / harf;
        greenValue += Mathf.Cos(Time.deltaTime) * colorLimit / harf;
        blueValue += Mathf.Sin(Time.deltaTime) * colorLimit / quarter;


        redValue %= colorLimit;
        greenValue %= colorLimit;
        blueValue %= colorLimit;
    }

    /// <summary>
    /// textの色を1秒ごとに変化させるコルーチン
    /// </summary>
    IEnumerator SwitchColor()
    {
        randomValue = Random.Range(0, colorList.Length);
        titleText.DOColor(colorList[randomValue],excutionTime);
        yield return new WaitForSeconds(excutionTime);
        StartCoroutine("SwitchColor");
    }
}