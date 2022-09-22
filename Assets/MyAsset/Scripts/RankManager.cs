using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{

    [SerializeField]
    private Monster monster;

    [SerializeField]
    private Text rankText;

    private float buffMax;
    private float buffValue;

    void Start()
    {
        StartCoroutine(ranking());
    }

    IEnumerator ranking()
    {
        yield return new WaitForSeconds(5f);
        buffMax = monster.BuffRange.y;
        buffValue = monster.Buff;

        if (buffValue / buffMax > 0.95)
        {
            rankText.text = "ランク : 海賊王";
        }
        else if (buffValue / buffMax > 0.8)
        {
            rankText.text = "ランク : 四皇";
        }
        else if (buffValue / buffMax > 0.65)
        {
            rankText.text = "ランク : 王下七武海";
        }
        else if (buffValue / buffMax > 0.45)
        {
            rankText.text = "ランク : 名の知れた海賊";
        }
        else if (buffValue / buffMax > 0.3)
        {
            rankText.text = "ランク : 一般海賊";
        }
        else if (buffValue / buffMax > 0.1)
        {
            rankText.text = "ランク : 雑魚海賊";
        }
        else
        {
            rankText.text = "ランク : 一般人";
        }
    }

}
