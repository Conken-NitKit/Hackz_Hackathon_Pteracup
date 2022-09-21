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
    /*
    [SerializeField]
    private ColorCode colorCode;
    */
    private string imageColor;



    void Start()
    {
        //imageColor = colorCode.HexadecimalCenterColor;
        monster.BuildStatus("#FF0000");
        parameterText.text = $"HP : {monster.BaseHp}\nAttack : {monster.BaseAtk}\nDefence : {monster.BaseDef}\nMP : {monster.BaseMp}\n“ÁŽê‹Z : {monster.SpacialCommand}";
    }

}
