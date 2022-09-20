using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class GenerateCharacter : MonoBehaviour
{
    private byte[] bytes;

    public async void Test()
    {
        var sceneB = await SceneLoader.Load<Main>("Main");
        Debug.Log(sceneB);
        sceneB.SetArguments(bytes);
    }

    void Start()
    {

    }
    

    void Update()
    {
        
    }
}
