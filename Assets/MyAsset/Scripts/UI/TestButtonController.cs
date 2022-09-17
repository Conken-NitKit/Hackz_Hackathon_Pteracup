using UnityEngine;
using System.Collections;
using MyAsset.Scripts.UI;
using UnityEngine.UI;

public class TestButtonController : BaseButtonController
{
    [SerializeField] private Monster objMonster;
    [SerializeField] private InputField input;
    [SerializeField] private Text statusText;
    [SerializeField] private Text commandText;
    protected override void OnClick(string objectName)
    {
        Debug.Log(objectName);
        if ("CheckButton".Equals(objectName))
        {
            // チェックがクリックされたとき
            this.CheckClick();
        } 
        else if ("AttackButton".Equals(objectName))
        {
            // 攻撃ボタンがクリックされたとき
            this.AttackClick();
        }
        else if ("GuardButton".Equals(objectName))
        {
            // 防御ボタンがクリックされたとき
            this.GuardClick();
        }
        else if ("SpacialButton".Equals(objectName))
        {
            // 特殊ボタンがクリックされたとき
            this.SpacialClick();
        }
        else
        {
            throw new System.Exception("Not implemented!!");
        }
    }

    private void CheckClick()
    {
        SpriteRenderer objSprite = objMonster.gameObject.GetComponent<SpriteRenderer>();
        objMonster.BuildStatus(input.text);
        statusText.text = $"HP:{objMonster.BaseHp},バフ量:{objMonster.Buff},攻撃力:{objMonster.BaseAtk},防御力:{objMonster.BaseDef},MP:{objMonster.BaseMp},特殊コマンド:{objMonster.SpacialCommand}";
        objSprite.color = objMonster.StatusColor;
        commandText.text = "ステータス生成";
    }

    private void AttackClick()
    {
        commandText.text = $"攻撃量:{objMonster.BaseAtk * objMonster.Buff}";
    }
    
    private void GuardClick()
    {
        commandText.text = $"防御量:{objMonster.BaseDef * objMonster.Buff}";
    }
    
    private void SpacialClick()
    {
        commandText.text = $"特殊タイプ:{objMonster.SpacialCommand.Type}";
    }
}