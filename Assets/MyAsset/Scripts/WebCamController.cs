using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WebCamController : MonoBehaviour
{

    public byte[] bytes;

    int width = 1920;
    int height = 1080;
    int fps = 30;
    Texture2D texture;
    WebCamTexture webcamTexture;
    Color32[] colors = null;

    [SerializeField]
    RawImage rawImage;

    [SerializeField]
    private Button switchGenerateCharacterButton;

    [SerializeField]
    private Text switchGenerateCharacterText;

    [SerializeField]
    private GameObject Button;

    IEnumerator Init()
    {
        while (true)
        {
            if (webcamTexture.width > 16 && webcamTexture.height > 16)
            {
                colors = new Color32[webcamTexture.width * webcamTexture.height];
                texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
                GetComponent<Renderer>().material.mainTexture = texture;
                break;
            }
            yield return null;
        }
    }

    // Use this for initialization
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
        this.rawImage = GetComponent<RawImage>();
        this.rawImage.texture = webcamTexture;
        this.rawImage.enabled = true;
        webcamTexture.Play();

        StartCoroutine(Init());
    }

    public void OnClick()
    {
        if (colors != null)
        {
            webcamTexture.GetPixels32(colors);

            int width = webcamTexture.width;
            int height = webcamTexture.height;
            Color32 rc = new Color32();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color c = colors[x + y * webcamTexture.width];
                    //colors[x + y * webcamTexture.width] = new Color(c.grayscale, c.grayscale, c.grayscale);
                }
            }
            texture.SetPixels32(colors);
            texture.Apply();
            bytes = texture.EncodeToPNG();
            rawImage.texture = texture;
            webcamTexture.Stop();
            switchGenerateCharacterButton.transform.DOLocalMove(new Vector3(245f, 27f, 0), 3f);
            switchGenerateCharacterText.DOFade(1f, 3f);
            Button.SetActive(true);
        }
    }

    public void TakeAgain()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
        this.rawImage = GetComponent<RawImage>();
        this.rawImage.texture = webcamTexture;
        this.rawImage.enabled = true;
        bytes = null;
        webcamTexture.Play();
        StartCoroutine(Init());
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        switchGenerateCharacterButton.transform.DOLocalMove(new Vector3(245f, 150f, 0), 3f);
        switchGenerateCharacterText.DOFade(0f, 3f);
        yield return new WaitForSeconds(3f);
        Button.SetActive(false);
    }

}