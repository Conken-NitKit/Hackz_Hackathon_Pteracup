using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;

/// <summary>
/// パソコン内部から画像を取得するクラス
/// </summary>
public class ImageReference : MonoBehaviour
{
    [SerializeField] 
    private RawImage rawImage;
    [SerializeField] 
    private ColorCode colorCode;
    
    void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Image Files", ".jpeg", ".png"));
        FileBrowser.SetDefaultFilter(".png");
    }

    /// <summary>
    /// 画像が選ばれたときに実行する
    /// </summary>
    /// <param name="paths"></param>
    void onSuccess(string[] paths) {
        byte[] bytes = File.ReadAllBytes(paths[0]);
        Texture2D texture = new Texture2D(200, 200);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);

        rawImage.texture = texture;
        colorCode.Tex = texture;
        colorCode.StartCoroutine(colorCode.GetCenterColor());
    }
    
    /// <summary>
    /// もしキャンセルされたら実行する
    /// </summary>
    void onCancel() {


    }
    
    /// <summary>
    /// 実行するとファイルを開く関数
    /// </summary>
    public void OpenFileDialog()
    {

        FileBrowser.ShowLoadDialog(onSuccess, onCancel, 0, false, null, "Load", "Select");

    }
}
