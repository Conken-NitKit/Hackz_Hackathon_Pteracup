using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

public class TestMonster : MonoBehaviour
{
    [SerializeField]
    private Monster monster;
    [SerializeField] 
    private ColorCode colorCode;
    
    public int MonsterInitialHp { get; set; }
    private ReactiveProperty<int> monsterNowHp = new ReactiveProperty<int>();
    
    public int MonsterInitialAtk { get; set; }
    private int monsterNowAtk;
    
    public int MonsterInitialDef { get; set; }
    private int monsterNowDef;
    
    public int MonsterInitialMp { get; set; }
    private int monsterNowMp;

    [SerializeField]
    private TestEnemy enemy;

    [SerializeField] 
    private TestGameMgr gameMgr;
    
    public void GenerateMonster()
    {
        monster.BuildStatus(colorCode.HexadecimalCenterColor);
        
        Debug.Log($"モンスター倍率:{monster.Buff}");
        Debug.Log($"モンスター基本HP:{monster.BaseHp}");
        Debug.Log($"モンスター基本Atk:{monster.BaseAtk}");
        Debug.Log($"モンスター基本Def:{monster.BaseDef}");
        Debug.Log($"モンスター基本MP:{monster.BaseMp}");
        
        MonsterInitialHp = (int)(monster.BaseHp * monster.Buff);
        monsterNowHp.Value = MonsterInitialHp;
        var subscription = monsterNowHp.Subscribe(x => {
            if (monsterNowHp.Value < 0)
            {
                monsterNowHp.Value = 0;
                Debug.Log("モンスターは倒れた！");
            }
        });
        Debug.Log($"モンスターHP:{MonsterInitialHp}");

        MonsterInitialAtk = (int)(monster.BaseAtk * monster.Buff);
        monsterNowAtk = MonsterInitialAtk;
        Debug.Log($"モンスターAtk:{MonsterInitialAtk}");

        MonsterInitialDef = (int)(monster.BaseDef * monster.Buff);
        monsterNowDef = MonsterInitialDef;
        Debug.Log($"モンスターDef:{MonsterInitialDef}");

        MonsterInitialMp = (int)(monster.BaseMp * monster.Buff);
        monsterNowMp = MonsterInitialMp;
        Debug.Log($"モンスターMP:{MonsterInitialMp}");
        
        Debug.Log("モンスターが生まれたよ！");
    }

    public void RunMonsterCommand(string methodName)
    {
        StartCoroutine(OnClicked(methodName));
    }
    
    private IEnumerator OnClicked(string methodName)
    {
        monsterNowAtk = MonsterInitialAtk;
        monsterNowDef = MonsterInitialDef;
        if ("AttackMonster" == methodName)
        {
            AttackMonster();
        }
        else if ("DefendMonster" == methodName)
        {
            DefendMonster();
        }
        else if ("DisplayStatus" == methodName)
        {
            DisplayStatus();
            yield break;
        }

        yield return new WaitForSeconds(2);
        gameMgr.isPlayerTurn.Value = false;
    }

    private void AttackMonster()
    {
        Debug.Log("モンスターの攻撃！");
        enemy.AttackedEnemy(monsterNowAtk);
    }

    private void DefendMonster()
    {
        Debug.Log("モンスターの防御！");
        monsterNowDef *= 2;
    }

    private void DisplayStatus()
    {
        Debug.Log($"モンスターHP:{monsterNowHp.Value}");
        Debug.Log($"モンスターAtk:{monsterNowAtk}");
        Debug.Log($"モンスターDef:{monsterNowDef}");
        Debug.Log($"モンスターMP:{monsterNowMp}");
    }

    public void AttackedMonster(int enemyAtk)
    {
        int damageReceived = (enemyAtk - monsterNowDef > 0) ? enemyAtk - monsterNowDef : 0;        
        Debug.Log($"モンスターは{damageReceived}ダメージ受けた！");
        monsterNowHp.Value -= damageReceived;
    }
}
