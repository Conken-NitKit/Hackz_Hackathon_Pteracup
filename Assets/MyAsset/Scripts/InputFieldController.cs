using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 入力した文字を名前として表示するスクリプト
/// </summary>
public class InputFieldController : MonoBehaviour
{

    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Text text;

    [SerializeField]
    private SwitchMainSceneButtonController switchMainSceneButtonController;


    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();
    }

    public void InputText()
    {
        if(inputField.text.Length > 0)
        {
            switchMainSceneButtonController.FadeIn();
        }
        else if(inputField.text.Length == 0)
        {
            switchMainSceneButtonController.FadeOut();
        }
        text.text = inputField.text;
    }

}
