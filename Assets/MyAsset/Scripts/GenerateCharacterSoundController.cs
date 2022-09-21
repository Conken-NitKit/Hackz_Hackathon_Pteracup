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
    

    /// <summary>
    /// 効果音を流した後にBGMを流すスクリプト
    /// </summary>
    /// <returns></returns>
    IEnumerator AudioPlay()
    {
        yield return new WaitForSeconds(3f);
        audioSource.PlayOneShot(sound1, volume);
        yield return new WaitForSeconds(3f);
        audioSource.Stop();
        audioSource.clip = sound2;
        audioSource.Play();
    }

}
