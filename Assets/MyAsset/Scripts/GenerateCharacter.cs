using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

/// <summary>
/// bytesの値をMainSceneのMain.csに渡すスクリプト
/// </summary>
public class GenerateCharacter : MonoBehaviour
{
    private byte[] bytes;

    [SerializeField]
    private FadeAnimation fadeAnimation;

    /// <summary>
    /// 値を渡す関数
    /// </summary>
    public async void Test()
    {
        var sceneB = await SceneLoader.Load<Main>("Main");
        sceneB.SetArguments(bytes);
    }

    /// <summary>
    /// FadeAnimationを動かすための関数
    /// </summary>
    public void SetArguments(byte[] date)
    {
        fadeAnimation.OnClick();
        bytes = date;
    }

    void Start()
    {

    }
   
}
