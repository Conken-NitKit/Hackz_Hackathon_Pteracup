using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// 戦うモンスター（シャンクス）のステータス受け取り、コマンドのクラス
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
    private BattleGameManager gameMgr;
    
    private IDisposable subscription;
    
    [SerializeField]
    private Text gameText;

    /// <summary>
    /// 新しい敵が現れた時に、新しい敵のBattleEnemyコンポーネントを代入する
    /// </summary>
    public void TargetEnemy()
    {
        enemy = gameMgr.enemy;
    }
    
    /// <summary>
    /// モンスターのステータスを決定するクラス
    /// 他クラスで決定された基礎ステータスからバフ等を掛けて実際にバトルするエネミーを生成
    /// </summary>
    public void DecideMonsterStatus()
    {
        if (subscription != null)
        {
            subscription.Dispose();
        }

        monster.BuildStatus(colorCode.HexadecimalCenterColor);
        
        Debug.Log($"シャンクス倍率:{monster.Buff}");
        Debug.Log($"シャンクス基本HP:{monster.BaseHp}");
        Debug.Log($"シャンクス基本Atk:{monster.BaseAtk}");
        Debug.Log($"シャンクス基本Def:{monster.BaseDef}");
        Debug.Log($"シャンクス基本MP:{monster.BaseMp}");
        
        MonsterInitialHp = (int)(monster.BaseHp * monster.Buff);
        monsterNowHp.Value = MonsterInitialHp;
        subscription = monsterNowHp.Subscribe(x => {
            if (monsterNowHp.Value < 0)
            {
                gameText.text = "シャンクスは倒れた！";
                monsterNowHp.Value = 0;
                diedMonster = false;
            }
        });

        MonsterInitialAtk = (int)(monster.BaseAtk * monster.Buff);
        monsterNowAtk = MonsterInitialAtk;

        MonsterInitialDef = (int)(monster.BaseDef * monster.Buff);
        monsterNowDef = MonsterInitialDef;

        MonsterInitialMp = (int)(monster.BaseMp * monster.Buff);
        monsterNowMp = MonsterInitialMp;

        diedMonster = false;
    }

    /// <summary>
    /// 外部からOnClickedMonsterCommandButtonを呼び出すときに使う
    /// </summary>
    public void RunMonsterCommand(string methodName)
    {
        StartCoroutine(OnClickedMonsterCommandButton(methodName));
    }
    
    /// <summary>
    /// 引数でコマンドメソッドの名前を指定することで実行するメソッド
    /// </summary>
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
    /// シャンクスの攻撃
    /// </summary>
    private void AttackMonster()
    {
        gameText.text =  "シャンクスの攻撃！";
        enemy.AttackedEnemy(monsterNowAtk);
    }

    /// <summary>
    /// シャンクスの防御
    /// </summary>
    private void DefendMonster()
    {
        gameText.text = "シャンクスの防御！";
        monsterNowDef *= 2;
    }

    /// <summary>
    /// シャンクスのステータスの一覧を表示するメソッド
    /// </summary>
    private void DisplayStatus()
    {
        Debug.Log($"シャンクスHP:{monsterNowHp.Value}");
        Debug.Log($"シャンクスAtk:{monsterNowAtk}");
        Debug.Log($"シャンクスDef:{monsterNowDef}");
        Debug.Log($"シャンクスMP:{monsterNowMp}");
    }

    /// <summary>
    /// 攻撃された時、このメソッドを呼び出して自分のhpを減らす
    /// </summary>
    /// <param name="enemyAtk"></param>
    public void AttackedMonster(int enemyAtk)
    {
        int damageReceived = (enemyAtk - monsterNowDef > 0) ? enemyAtk - monsterNowDef : 0;        
        gameText.text = $"シャンクスは{damageReceived}ダメージ受けた！";
        monsterNowHp.Value -= damageReceived;
    }
}
