using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

/// <summary>
/// 
/// </summary>
public class BattleMonster : MonoBehaviour
{
    [SerializeField]
    private Monster monster;
    [SerializeField] 
    private ColorCode colorCode;
    
    private int MonsterInitialHp { get; set; }
    private ReactiveProperty<int> monsterNowHp = new ReactiveProperty<int>();
    
    private int MonsterInitialAtk { get; set; }
    private int monsterNowAtk;
    
    private int MonsterInitialDef { get; set; }
    private int monsterNowDef;
    
    private int MonsterInitialMp { get; set; }
    private int monsterNowMp;

    private bool diedMonster;
    
    private BattleEnemy enemy;

    [SerializeField] 
    private TestGameMgr gameMgr;
    
    private IDisposable subscription;

    /// <summary>
    /// 
    /// </summary>
    public void TargetEnemy()
    {
        enemy = gameMgr.enemy;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void GenerateMonster()
    {
        
        if (!(subscription == null))
        {
            subscription.Dispose();
        }
        
        monster.BuildStatus(colorCode.HexadecimalCenterColor);
        
        Debug.Log($"モンスター倍率:{monster.Buff}");
        Debug.Log($"モンスター基本HP:{monster.BaseHp}");
        Debug.Log($"モンスター基本Atk:{monster.BaseAtk}");
        Debug.Log($"モンスター基本Def:{monster.BaseDef}");
        Debug.Log($"モンスター基本MP:{monster.BaseMp}");
        
        MonsterInitialHp = (int)(monster.BaseHp * monster.Buff);
        monsterNowHp.Value = MonsterInitialHp;
        subscription = monsterNowHp.Subscribe(x => {
            if (monsterNowHp.Value < 0)
            {
                Debug.Log("モンスターは倒れた！");
                monsterNowHp.Value = 0;
                diedMonster = false;
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

        diedMonster = false;
        
        Debug.Log("モンスターが生まれたよ！");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodName"></param>
    public void RunMonsterCommand(string methodName)
    {
        StartCoroutine(OnClickedMonsterCommandButton(methodName));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodName"></param>
    /// <returns></returns>
    private IEnumerator OnClickedMonsterCommandButton(string methodName)
    {
        if (gameMgr.isPlayerTurn.Value || !diedMonster)
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
    }

    /// <summary>
    /// 
    /// </summary>
    private void AttackMonster()
    {
        Debug.Log("モンスターの攻撃！");
        enemy.AttackedEnemy(monsterNowAtk);
    }

    /// <summary>
    /// 
    /// </summary>
    private void DefendMonster()
    {
        Debug.Log("モンスターの防御！");
        monsterNowDef *= 2;
    }

    /// <summary>
    /// 
    /// </summary>
    private void DisplayStatus()
    {
        Debug.Log($"モンスターHP:{monsterNowHp.Value}");
        Debug.Log($"モンスターAtk:{monsterNowAtk}");
        Debug.Log($"モンスターDef:{monsterNowDef}");
        Debug.Log($"モンスターMP:{monsterNowMp}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemyAtk"></param>
    public void AttackedMonster(int enemyAtk)
    {
        int damageReceived = (enemyAtk - monsterNowDef > 0) ? enemyAtk - monsterNowDef : 0;        
        Debug.Log($"モンスターは{damageReceived}ダメージ受けた！");
        monsterNowHp.Value -= damageReceived;
    }
}
