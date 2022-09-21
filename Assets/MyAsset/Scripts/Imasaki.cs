using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;

public class Imasaki : MonoBehaviour
{

    [SerializeField]
    private WebCamController webCamController;

    private byte[] bytes;

    public async void Test()
    {
        bytes = webCamController.bytes;
        var sceneA = await SceneLoader.Load<GenerateCharacter>("GenerateCharacter");
        sceneA.SetArguments(bytes);
    }

    void Start()
    {
        
    }

}
