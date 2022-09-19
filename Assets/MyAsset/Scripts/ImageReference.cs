using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;　//これも必要だった
using SimpleFileBrowser;
using UnityEngine.UI;

public class ImageReference : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private ColorCode colorCode;
    
    void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Image Files", ".jpeg", ".png"));
        FileBrowser.SetDefaultFilter(".png");
    }

    void onSuccess(string[] paths) {
        byte[] bytes = File.ReadAllBytes(paths[0]);
        Texture2D texture = new Texture2D(200, 200);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);

        rawImage.texture = texture;
        colorCode.tex = texture;
        colorCode.StartCoroutine(colorCode.GetCenterColor());
    }
    
    void onCancel() {


    }
    
    
    public void OpenFileDialog()
    {

        FileBrowser.ShowLoadDialog(onSuccess, onCancel, 0, false, null, "Load", "Select");

    }
}
