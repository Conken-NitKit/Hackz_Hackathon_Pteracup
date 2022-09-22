using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

/// <summary>
/// 戦うモンスター（シャンクス）のステータス受け取り、コマンドのクラス
/// </summary>
public class BattleMonster : MonoBehaviour
{
    private int monsterInitialHp;
    private ReactiveProperty<int> monsterNowHp = new ReactiveProperty<int>();

    private int monsterInitialAtk;
    private int monsterNowAtk;

    private int monsterInitialDef;
    private int monsterNowDef;

    private int monsterInitialMp;
    private ReactiveProperty<int> monsterNowMp = new ReactiveProperty<int>();

    private bool diedMonster;
    
    [SerializeField] 
    private GameObject buttonBlockPanel;
    
    private BattleEnemy enemy;

    [SerializeField] 
    private BattleGameManager gameMgr;
    
    private IDisposable subscriptionHp;
    private IDisposable subscriptionMp;

    [SerializeField]
    private SceneAnimation sceneAnimation;
    
    [SerializeField]
    private Text gameText;
    
    [SerializeField]
    private Text hpText;
    
    [SerializeField]
    private Text mpText;

    [SerializeField]
    private Main main;

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
    public void DecideMonsterStatus(Monster monster)
    {
        if (subscriptionHp != null)
        {
            subscriptionHp.Dispose();
        }

        if (subscriptionMp != null)
        {
            subscriptionMp.Dispose();
        }

        Debug.Log($"シャンクス倍率:{monster.Buff}");
        Debug.Log($"シャンクス基本HP:{monster.BaseHp}");
        Debug.Log($"シャンクス基本Atk:{monster.BaseAtk}");
        Debug.Log($"シャンクス基本Def:{monster.BaseDef}");
        Debug.Log($"シャンクス基本MP:{monster.BaseMp}");
        
        monsterInitialHp = (int)(monster.BaseHp * monster.Buff);
        monsterNowHp.Value = monsterInitialHp;
        subscriptionHp = monsterNowHp.Subscribe(x => {
            if (monsterNowHp.Value <= 0)
            {
                gameText.text = "シャンクスは倒れた！";
                monsterNowHp.Value = 0;
                sceneAnimation.RiseCurtain();
                main.PassMainToGameOver();
                diedMonster = false;
            }

            hpText.text = $"{monsterNowHp.Value}";
        });
        
        subscriptionMp = monsterNowMp.Subscribe(x => {
            mpText.text = $"{monsterNowMp.Value}";
        });

        monsterInitialAtk = (int)(monster.BaseAtk * monster.Buff);
        monsterNowAtk = monsterInitialAtk;

        monsterInitialDef = (int)(monster.BaseDef * monster.Buff);
        monsterNowDef = monsterInitialDef;

        monsterInitialMp = (int)(monster.BaseMp * monster.Buff);
        monsterNowMp.Value = monsterInitialMp;

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
            monsterNowAtk = monsterInitialAtk;
            monsterNowDef = monsterInitialDef;
            if ("AttackMonster" == methodName)
            {
                AttackMonster();
            }
            else if ("DefendMonster" == methodName)
            {
                DefendMonster();
            }
            else if ("DoSpeCial" == methodName)
            {
                if (monsterNowMp.Value - 5 < 0)
                {
                    gameText.text =  "MPが足りん！";
                    yield break;
                }

                DoSpeCial();
                monsterNowMp.Value -= 5;
            }
            
            buttonBlockPanel.SetActive(true);

            yield return new WaitForSeconds(2);
            if (enemy.dieEnemy)
            {
                gameMgr.isPlayerTurn.SetValueAndForceNotify(true);
                yield break;
            }
            gameMgr.isPlayerTurn.Value = false;
        }
    }

    /// <summary>
    /// シャンクスの攻撃
    /// </summary>
    private void AttackMonster()
    {
        gameText.text =  "シャンクスの攻撃！";
        this.transform.DOMoveX(-0.5f,0.2f).SetRelative(true).SetLoops(2,LoopType.Yoyo);
        enemy.AttackedEnemy(monsterNowAtk);
    }

    /// <summary>
    /// シャンクスの防御
    /// </summary>
    private void DefendMonster()
    {
        gameText.text = "シャンクスの防御！";
        this.transform.DOScale(new Vector3(1.2f,1.2f,1.2f), 0.2f).SetLoops(2,LoopType.Yoyo);
        monsterNowDef *= 2;
    }

    private void DoSpeCial()
    {
        gameText.text = "シャンクスの回復！";
        this.transform.DOMoveY(0.5f,0.05f).SetRelative(true).SetLoops(10,LoopType.Yoyo);
        this.transform.DOScale(new Vector3(1.2f,1.2f,1.2f), 0.05f).SetLoops(10,LoopType.Yoyo);
        monsterNowHp.Value += 10;
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
        this.transform.DOMoveX(0.3f, 1f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
        this.transform.DORotate(new Vector3(0, 0, 10), 0.1f).SetRelative(true).SetLoops(10, LoopType.Yoyo);
        monsterNowHp.Value -= damageReceived;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            monsterNowHp.Value = 0;
        }
    }
}
