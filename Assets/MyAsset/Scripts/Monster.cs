using System;
using System.Linq;
using UnityEngine;
using GD.MinMaxSlider;
using MyAsset.Scripts.DevUtil;
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
    [MinMaxSlider(0, 100)] [SerializeField] private Vector2Int hpRange = new Vector2Int(10,25);

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

    [Header("特殊コマンドリスト")] [SerializeField] private BaseCommand[] commands;
    private readonly int[] randNums = new int[] {8,2,7,15,10,0,12,1,13,3,5,11,9,6,14,4};
    [SerializeField] private double MaxDiff;
    
    /// <summary>
    /// 引数として受け取ったカラーコードからステータスを生成するメソッド
    /// </summary>
    /// <param name="colorCode">カラーコード</param>
    public void BuildStatus(string colorCode)
    {
        if (!ColorUtility.TryParseHtmlString( colorCode , out var color ))
        {
            Debug.Log("与えられた文字列は色として認識できません！");
            return;
        }
        StatusColor = color;
        
        if (colorCode[0] == '#')
        {
            colorCode = colorCode.Remove(0,1);
        }
        
        int[] rNum = HexToIntArray(colorCode.Substring(0, 2));
        int[] gNum = HexToIntArray(colorCode.Substring(2, 2));
        int[] bNum = HexToIntArray(colorCode.Substring(4, 2));
        
        double[] red = new double[] {255, 0, 0};
        double[] color32 = new double[] {StatusColor.r, StatusColor.g, StatusColor.b};
        double buffNum = new ColorUtil().CalcColorDifferent(color32,red);
        float minBuff = buffRange.x,maxBuff = buffRange.y;
        Buff = (100 - (float)buffNum) / 100f * maxBuff + minBuff;
        //SpacialCommand = commands[rNum[1] % commands.Length];

        int seedMaxNum = 15;
        BaseHp = CalcStatus(randNums[gNum[0]], seedMaxNum, hpRange);
        BaseDef = CalcStatus(randNums[gNum[1]], seedMaxNum, defRange);
        BaseAtk = CalcStatus(randNums[bNum[0]], seedMaxNum, atkRange);
        BaseMp = CalcStatus(randNums[bNum[1]], seedMaxNum, mpRange);
    }

    /// <summary>
    /// 個体値(16進数の一桁)を基にステータスを計算して返すメソッド
    /// </summary>
    /// <param name="value">個体値を決定する数字</param>
    /// <param name="seedMax">個体値の最大値 基本的には15をとる</param>
    /// <param name="minMax">最
    /// 小最大値を管理するVector2Int</param>
    /// <returns></returns>
    private int CalcStatus(int value,int seedMax, Vector2Int minMax)
    {
        int min = minMax.x,max = minMax.y;
        int offset = (int) Math.Round((double) value / (double) seedMax * max-min);
        return offset + min;
    }
      
    /// <summary>
    /// 16進数文字列を1文字ごとにintに変換しint型の配列で返す関数
    /// </summary>
    /// <param name="hexStr">16進数の文字列</param>
    /// <returns></returns>
    private int[] HexToIntArray(string hexStr)
    {
        int[] intArray = new int[hexStr.Length];
        foreach (var hexItem in hexStr.Select((hexChar, index) => new {hexChar, index}))
        {
            intArray[hexItem.index] = Convert.ToInt32(hexItem.hexChar.ToString(), 16);
        }

        return intArray;
    }
}
