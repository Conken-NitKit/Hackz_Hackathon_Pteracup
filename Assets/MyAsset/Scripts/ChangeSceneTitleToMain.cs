using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Titleからmainにシーン遷移するスクリプト
/// </summary>
public class ChangeSceneTitleToMain : MonoBehaviour
{

    void Start()
    {

    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("Main");
    }
}
