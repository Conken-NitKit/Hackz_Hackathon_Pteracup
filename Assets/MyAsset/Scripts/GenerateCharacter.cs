using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;


/// <summary>
/// bytesの値をMainSceneのMain.csに渡すスクリプト
/// </summary>
public class GenerateCharacter : MonoBehaviour
{
    private byte[] bytes;

    [SerializeField]
    private FadeAnimation fadeAnimation;
    /*
    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private ColorCode colorCode;
    */
    [SerializeField]
    private Monster monster;

    [SerializeField]
    private Text inputFieldText;


    /// <summary>
    /// 値を渡す関数
    /// </summary>
    public async void Test()
    {
        var sceneB = await SceneLoader.Load<Main>("Main");
        //sceneB.SetArguments(colorCode.HexadecimalCenterColor,inputFieldText.text, monster);
        sceneB.SetArguments("#FF0000",inputFieldText.text, monster);
    }

    /// <summary>
    /// FadeAnimationを動かすための関数
    /// </summary>
    public void SetArguments(byte[] date)
    {
        /*
        bytes = date;
        Texture2D texture = new Texture2D(200, 200);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);

        rawImage.texture = texture;
        colorCode.Tex = texture;
        colorCode.StartCoroutine(colorCode.GetCenterColor());
        */
        fadeAnimation.Fade();
    }

    void Start()
    {

    }
   
}
