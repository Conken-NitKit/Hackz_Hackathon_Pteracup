using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TitleSceneからGenerateCharacterSceneへのシーン遷移のスクリプト
/// 遷移先にある取得するスクリプトはGameObjectにアタッチされていないと動作しない
/// </summary>
public class Title : MonoBehaviour
{
    /*
    [SerializeField]
    private ImageReference imageReference;
    */
    private byte[] bytes;

    public async void Test()
    {
       // bytes = imageReference.bytes;
        var sceneA = await SceneLoader.Load<GenerateCharacter>("GenerateCharacter");
        sceneA.SetArguments(bytes);
    }

    void Start()
    {

    }


}
