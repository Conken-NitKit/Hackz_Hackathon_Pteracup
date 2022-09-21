using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GenerateCharacterSceneの効果音とBGMをつなげて流すスクリプト
/// </summary>
public class GenerateCharacterSoundController : MonoBehaviour
{

    [SerializeField]
    private AudioClip sound1;
    [SerializeField]
    private AudioClip sound2;

    [SerializeField]
    private float volume = 0.5f;
    [SerializeField]
    private float waitTime = 4f;

    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        StartCoroutine(AudioPlay());
    }


    void Update()
    {
        
    }

    /// <summary>
    /// 効果音を流した後にBGMを流すスクリプト
    /// </summary>
    /// <returns></returns>
    IEnumerator AudioPlay()
    {
        audioSource.PlayOneShot(sound1, volume);
        yield return new WaitForSeconds(4f);
        audioSource.Stop();
        audioSource.PlayOneShot(sound2, volume);
    }

}
