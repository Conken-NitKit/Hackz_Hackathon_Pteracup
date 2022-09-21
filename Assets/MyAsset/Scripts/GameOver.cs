using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

/// <summary>
/// GameOverScene����TitleScene�ɑJ�ڂ���X�N���v�g
/// </summary>
public class GameOver : MonoBehaviour
{

    public async void Switch()
    {
        var sceneC = await SceneLoader.Load<Title>("Title");
    }
    
    public void SetArguments(int score,string characterNam)
    {
    }

    void Start()
    {
        
    }
}
