using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TitleScene�̔w�i�ƃ{�^���A�^�C�g���̐F��ύX����X�N���v�g
/// </summary>
public class BackGroundColorController : MonoBehaviour
{

    private float redValue;
    private float greenValue;
    private float blueValue;
    private float colorLimit = 255;

    [SerializeField]
    private Slider redGauge;
    [SerializeField]
    private Slider greenGauge;
    [SerializeField]
    private Slider blueGauge;

    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button imageSelectionButton;
    [SerializeField]
    private Button rankingButton;

    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text startText;
    [SerializeField]
    private Text imageSelectionText;
    [SerializeField]
    private Text rankingText;

    [SerializeField]
    private GameObject background;

    void Start()
    {
        
    }

    /// <summary>
    /// TitleScene�̍����ɂ���o�[���������ꂽ�Ƃ��ɂ��̒l�ɂ���ĐF��ύX����֐�
    /// </summary>
    public void ColorUpdate()
    {
        redValue = redGauge.value;
        greenValue = greenGauge.value;
        blueValue = blueGauge.value;

        startText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
        imageSelectionText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
        rankingText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);

        //background.GetComponent<SpriteRenderer>().color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);

        redValue += colorLimit / 2;
        greenValue += colorLimit / 2;
        blueValue += colorLimit / 2;
        redValue %= colorLimit;
        greenValue %= colorLimit;
        blueValue %= colorLimit;

        titleText.color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);

        startButton.GetComponent<Image>().color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
        imageSelectionButton.GetComponent<Image>().color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
        //rankingButton.GetComponent<Image>().color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
    }
}
