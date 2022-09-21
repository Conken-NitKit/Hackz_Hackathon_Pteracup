using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

/// <summary>
/// GameOverSceneからTitleSceneに遷移するスクリプト
/// </summary>
public class GameOver : MonoBehaviour
{

    public async void Switch()
    {
        var sceneC = await SceneLoader.Load<Title>("Title");
    }

    void Start()
    {
        
    }
}
