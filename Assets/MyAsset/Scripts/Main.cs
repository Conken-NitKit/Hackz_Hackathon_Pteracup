using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GenerateCharacter.csから渡される値をもらうスクリプト
/// </summary>
public class Main : MonoBehaviour
{
    /// <summary>
    /// 値をもらう関数
    /// </summary>
    /// <param name="bytes"></param>
    public void SetArguments(byte[] bytes)
    {
        Debug.Log(bytes);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
