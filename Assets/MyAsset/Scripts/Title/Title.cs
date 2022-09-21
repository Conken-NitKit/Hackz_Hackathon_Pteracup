using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TitleSceneからGenerateCharacterSceneへのシーン遷移のスクリプト
/// 遷移先にある取得するスクリプトはGameObjectにアタッチされていないと動作しない
/// </summary>
public class Title : MonoBehaviour
{
    
    [SerializeField]
    private ImageReference imageReference;

    public async void Test()
    {
        var sceneA = await SceneLoader.Load<GenerateCharacter>("GenerateCharacter");
        sceneA.SetArguments(imageReference.bytes);
    }
}
