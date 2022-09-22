using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterDisplay : MonoBehaviour
{

    [SerializeField]
    private Text parameterText;

    [SerializeField]
    private Monster monster;
    
    [SerializeField]
    private ColorCode colorCode;

    public void DecideStatus()
    {
        monster.BuildStatus(colorCode.HexadecimalCenterColor);
        parameterText.text = $"HP : {monster.BaseHp * (int)monster.Buff}\nAttack : {monster.BaseAtk * (int)monster.Buff}\nDefence : {monster.BaseDef * (int)monster.Buff}\nMP : {monster.BaseMp * (int)monster.Buff}\n特殊コマンド : Heal";
    }

}
