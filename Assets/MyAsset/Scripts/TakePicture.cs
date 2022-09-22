using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{
    [SerializeField]
    private WebCamController webCamController;

    private byte[] bytes;

    public async void PassPictureToGenerate()
    {
        bytes = webCamController.bytes;
        var sceneA = await SceneLoader.Load<GenerateCharacter>("GenerateCharacter");
        sceneA.SetArguments(bytes);
    }

    public async void PassPictureToTitle()
    {
        var sceneB = await SceneLoader.Load<Title>("Title");
    }

    void Start()
    {

    }
}
