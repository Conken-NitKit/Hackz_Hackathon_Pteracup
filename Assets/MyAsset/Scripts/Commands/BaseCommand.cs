using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 特殊コマンドの基底クラス
/// </summary>
[CreateAssetMenu(menuName = "MyScriptable/Command")]
public class BaseCommand : ScriptableObject {
    /// <summary>
    /// 回復・ステータスバフ・攻撃等のコマンドのタイプ
    /// </summary>
    public enum CommandType {
        Heal,
        MpHeal,
        DefBuff,
        AttackBuff,
        Attack
    }
    public string CommandName { get; set; }
    public readonly CommandType Type;
}
