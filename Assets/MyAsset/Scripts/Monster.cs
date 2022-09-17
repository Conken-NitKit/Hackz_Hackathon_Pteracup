using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GD.MinMaxSlider;
using UnityEngine.Serialization;

/// <summary>
/// モンスターの基礎ステータスを管理するクラス
/// </summary>
public class Monster : MonoBehaviour
{
    /// <summary>
    /// ステータスバフ量
    /// </summary>
    [field: SerializeField, Header("強化倍率")] public float Buff { get; private set; }
    [MinMaxSlider(0, 10)] [SerializeField] private Vector2 buffRange = new Vector2(1.0f,2.5f);

    /// <summary>
    /// 基礎HP
    /// </summary>
    [field: SerializeField, Header("HP")] public int BaseHp { get; private set; }
    [MinMaxSlider(0, 100)] [SerializeField] private Vector2Int hpRange = new Vector2Int(10,35);

    /// <summary>
    /// 基礎攻撃力
    /// </summary>
    [field: SerializeField, Header("攻撃力")] public int BaseAtk { get; private set; }
    [MinMaxSlider(0, 100)] [SerializeField] private Vector2Int atkRange = new Vector2Int(1,16);

    /// <summary>
    /// 基礎防御力
    /// </summary>
    [field: SerializeField, Header("防御力")] public int BaseDef { get; private set; }
    [MinMaxSlider(0, 100)] [SerializeField] private Vector2Int defRange = new Vector2Int(0,15);

    /// <summary>
    /// 基礎MP
    /// </summary>
    [field: SerializeField, Header("MP")] public int BaseMp { get; private set; }
    [MinMaxSlider(0, 100)] [SerializeField] private Vector2Int mpRange = new Vector2Int(0,15);
    
    /// <summary>
    /// ステータス生成カラー
    /// </summary>
    [field: SerializeField, Header("ステータスカラー")] public Color32 StatusColor { get; private set; }

    /// <summary>
    /// 保有特殊コマンド
    /// </summary>
    [field: SerializeField, Header("特殊コマンド")] public BaseCommand SpacialCommand { get; private set; }

    [FormerlySerializedAs("Commands")] [Header("特殊コマンドリスト")] [SerializeField] BaseCommand[] commands;
    private readonly int[] _randNums = new int[] {8,2,7,15,10,0,12,1,13,3,5,11,9,6,14,4};
    
    /// <summary>
    /// 引数として受け取ったカラーコードからステータスを生成するメソッド
    /// </summary>
    /// <param name="colorCode">カラーコード</param>
    public void BuildStatus(string colorCode)
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
        int[] rNum = new int[]{Convert.ToInt32(r[0].ToString(), 16),Convert.ToInt32(r[1].ToString(), 16)};
        float minBuff = buffRange.x,maxBuff = buffRange.y;
        
        Buff = rNum[0] / 15f * maxBuff + minBuff;
        SpacialCommand = commands[rNum[1] % commands.Length];
    }
    private void BuildStatusGreen(string colorCode)
    {
        string g = colorCode.Substring(2, 2);
        int[] gNum = new int[]{Convert.ToInt32(g[0].ToString(), 16),Convert.ToInt32(g[1].ToString(), 16)};
        int minHp = hpRange.x,maxHp = hpRange.y; 
        int minDef = defRange.x,maxDef = defRange.y;

        BaseHp = CalcOffset(_randNums[gNum[0]],15,maxHp-minHp) + minHp;
        BaseDef = CalcOffset(_randNums[gNum[1]],15,maxDef-minDef) + minDef;
    }
    private void BuildStatusBlue(string colorCode)
    {
        string b = colorCode.Substring(4, 2); 
        int[] bNum = new int[]{Convert.ToInt32(b[0].ToString(), 16),Convert.ToInt32(b[1].ToString(), 16)};
        int minAtk = atkRange.x,maxAtk = atkRange.y; 
        int minMp = mpRange.x,maxMp = mpRange.y;
        
        BaseAtk = CalcOffset(_randNums[bNum[0]],15,maxAtk-minAtk) + minAtk;
        BaseMp = CalcOffset(_randNums[bNum[1]],15,maxMp-minMp) + minMp;
    }

    private int CalcOffset(int numerator,int denominator,double range)
    {
        return (int)Math.Round((double) numerator / (double) denominator * range); 
    }
}
