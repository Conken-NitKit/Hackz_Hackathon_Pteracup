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

    /// <summary>
    /// 値を渡す関数
    /// </summary>
    public async void Test()
    {
        var sceneB = await SceneLoader.Load<Main>("Main");
        Debug.Log(sceneB);
        sceneB.SetArguments(bytes);
    }

    void Start()
    {

    }
   
}
