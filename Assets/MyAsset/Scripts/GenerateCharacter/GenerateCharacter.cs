using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.IO;
using DG.Tweening;
using SimpleFileBrowser;
using UnityEngine.UI;


/// <summary>
/// bytesの値をMainSceneのMain.csに渡すスクリプト
/// </summary>
public class GenerateCharacter : MonoBehaviour
{
    [SerializeField]
    private Fade fade;
    
    private byte[] bytes;

    [SerializeField]
    private FadeAnimation fadeAnimation;
    [SerializeField]
    private ColorCode colorCode;
    
    [SerializeField]
    private Monster monster;

    [SerializeField]
    private Text inputFieldText;
    
    [SerializeField]
    private Text[] texts;
    
    [SerializeField]
    private Image[] images;

    /// <summary>
    /// 値を渡す関数
    /// </summary>
    public async void PassGenerateToMain()
    {
        foreach (var text in texts)
        {
            text.DOFade(0, 1f);
        }
        foreach (var image in images)
        {
            image.DOFade(0, 1f);
        }
        
        fade.FadeIn(2f);
        await Task.Delay(2000);
        var sceneB = await SceneLoader.Load<Main>("Main");
        sceneB.SetArguments(colorCode.HexadecimalCenterColor,inputFieldText.text, monster);
    }

    /// <summary>
    /// FadeAnimationを動かすための関数
    /// </summary>
    public void SetArguments(byte[] bytes)
    {
        Texture2D texture = new Texture2D(200, 200);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);
        colorCode.StartCoroutine(colorCode.GetCenterColor(texture));
        fadeAnimation.Fade();
    }
}
