using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title����main�ɃV�[���J�ڂ���X�N���v�g
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
