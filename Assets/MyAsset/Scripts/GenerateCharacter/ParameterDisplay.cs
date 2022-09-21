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
        parameterText.text = $"HP : {monster.BaseHp}\nAttack : {monster.BaseAtk}\nDefence : {monster.BaseDef}\nMP : {monster.BaseMp}\n特殊コマンド : {monster.SpacialCommand}";
    }

}
