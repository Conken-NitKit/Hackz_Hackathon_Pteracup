using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// textの色を変化させるスクリプト
/// </summary>
public class TextColorController : MonoBehaviour
{


    private float redValue = 0;
    private float greenValue = 0;
    private float blueValue = 0;
    private float colorLimit = 255;
    private float waitTime = 1;
    private float harf = 2;
    private float quarter = 4;

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
        redValue = Random.Range(0, 255);
        greenValue = Random.Range(0, 255);
        blueValue = Random.Range(0, 255);
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
        yield return new WaitForSeconds(waitTime);
        //startText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, (byte)colorLimit);
        //imageSelectionText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, (byte)colorLimit);
        titleText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, (byte)colorLimit);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("SwitchColor");
    }
}
