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

    void Start()
    {
        monster.BuildStatus("#FF3333");
        parameterText.text = $"HP : {monster.BaseHp}\nAttac : {monster.BaseAtk}\nDefence : {monster.BaseDef}\nMP : {monster.BaseMp}\n“ÁŽê‹Z : {monster.SpacialCommand}";
    }


    void Update()
    {
        
    }
}
