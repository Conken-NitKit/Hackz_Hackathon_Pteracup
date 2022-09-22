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
            rankText.text = "�����N : �C����";
        }
        else if (buffValue / buffMax > 0.8)
        {
            rankText.text = "�����N : �l�c";
        }
        else if (buffValue / buffMax > 0.65)
        {
            rankText.text = "�����N : ���������C";
        }
        else if (buffValue / buffMax > 0.45)
        {
            rankText.text = "�����N : ���̒m�ꂽ�C��";
        }
        else if (buffValue / buffMax > 0.3)
        {
            rankText.text = "�����N : ��ʊC��";
        }
        else if (buffValue / buffMax > 0.1)
        {
            rankText.text = "�����N : �G���C��";
        }
        else
        {
            rankText.text = "�����N : ��ʐl";
        }
    }

}
