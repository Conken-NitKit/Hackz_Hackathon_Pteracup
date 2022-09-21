using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GenerateCharacter.csから渡される値をもらうスクリプト
/// </summary>
public class Main : MonoBehaviour
{

    private byte[] bytes;

    /// <summary>
    /// 値をもらう関数
    /// </summary>
    /// <param name="bytes"></param>
    public void SetArguments(byte[] date)
    {
        bytes = date;
    }

    void Start()
    {
        
    }

}
