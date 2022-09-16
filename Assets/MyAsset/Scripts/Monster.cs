using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GD.MinMaxSlider;

public class Monster : MonoBehaviour
{
    [field: SerializeField, Header("強化倍率")]
    public float StatusBuff { get; private set; }

    [MinMaxSlider(0, 10)] [SerializeField] private Vector2 BuffRange = new Vector2(1.0f,2.5f);

    [field: SerializeField, Header("HP")] public int BaseHp { get; private set; }

    [MinMaxSlider(0, 100)] [SerializeField]
    private Vector2Int HpRange = new Vector2Int(10,35);

    [field: SerializeField, Header("攻撃力")] public int BaseAtk { get; private set; }

    [MinMaxSlider(0, 100)] [SerializeField]
    private Vector2Int AtkRange = new Vector2Int(1,16);

    [field: SerializeField, Header("防御力")] public int BaseDef { get; private set; }

    [MinMaxSlider(0, 100)] [SerializeField]
    private Vector2Int DefRange = new Vector2Int(0,15);

    [field: SerializeField, Header("MP")] public int BaseMp { get; private set; }

    [MinMaxSlider(0, 100)] [SerializeField]
    private Vector2Int MpRange = new Vector2Int(0,15);

    [field: SerializeField, Header("ステータスカラー")]
    public Color32 StatusColor { get; private set; }

    [field: SerializeField, Header("特殊コマンド")]
    public BaseCommand SpacialCommand { get; private set; }

    [Header("特殊コマンドリスト")] [SerializeField] BaseCommand[] Commands;
    private readonly int[] randNums = new int[] {8,2,7,15,10,0,12,1,13,3,5,11,9,6,14,4};
    
    public void Initialize(string colorCode)
    {
        Color color;
        if (!ColorUtility.TryParseHtmlString( colorCode , out color ))
        {
            Debug.Log("与えられた文字列は色として認識できません！");
            return;
        }
        StatusColor = color;
        
        if (colorCode[0] == '#')
        {
            colorCode = colorCode.Remove(0,1);
        }

        BuildStatusRed(colorCode);
        BuildStatusGreen(colorCode);
        BuildStatusBlue(colorCode);
    }
    
    private void BuildStatusRed(string colorCode)
    {
        string r = colorCode.Substring(0, 2);
        float minBuff = BuffRange.x,maxBuff = BuffRange.y;
        
        int[] rNum = new int[]{Convert.ToInt32(r[0].ToString(), 16),Convert.ToInt32(r[1].ToString(), 16)};
        StatusBuff = rNum[0] / 15f * maxBuff + minBuff;
        SpacialCommand = Commands[rNum[1] % Commands.Length];
    }
    private void BuildStatusGreen(string colorCode)
    {
        string g = colorCode.Substring(2, 2);
        int minHp = HpRange.x,maxHp = HpRange.y; 
        int minDef = DefRange.x,maxDef = DefRange.y;

        int[] gNum = new int[]{Convert.ToInt32(g[0].ToString(), 16),Convert.ToInt32(g[1].ToString(), 16)};
        BaseHp = CalcOffset(randNums[gNum[0]],15,maxHp-minHp) + minHp;
        BaseDef = CalcOffset(randNums[gNum[1]],15,maxDef-minDef) + minDef;
    }
    private void BuildStatusBlue(string colorCode)
    {
        string b = colorCode.Substring(4, 2); 
        int minAtk = AtkRange.x,maxAtk = AtkRange.y; 
        int minMp = MpRange.x,maxMp = MpRange.y;
        
        int[] bNum = new int[]{Convert.ToInt32(b[0].ToString(), 16),Convert.ToInt32(b[1].ToString(), 16)};
        BaseAtk = CalcOffset(randNums[bNum[0]],15,maxAtk-minAtk) + minAtk;
        BaseMp = CalcOffset(randNums[bNum[1]],15,maxMp-minMp) + minMp;
    }

    private int CalcOffset(int numerator,int denominator,double range)
    {
        return (int)Math.Round((double) numerator / (double) denominator * range); 
    }
}


