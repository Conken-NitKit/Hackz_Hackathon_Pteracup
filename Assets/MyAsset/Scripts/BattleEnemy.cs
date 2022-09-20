using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;

public class BattleEnemy : MonoBehaviour
{
    private int EnemyInitialHp { get; set; }
    public ReactiveProperty<int> enemyNowHp = new ReactiveProperty<int>();
    private Vector2Int hpRange = new Vector2Int(20,50);
    
    private int EnemyInitialAtk { get; set; }
    public int enemyNowAtk;
    private Vector2Int atkRange = new Vector2Int(5,15);
    
    private int EnemyInitialDef { get; set; }
    public int enemyNowDef;
    private Vector2Int defRange = new Vector2Int(0,10);

    private bool dieEnemy;

    [SerializeField]
    private BattleMonster monster;
    
    [SerializeField] 
    private TestGameMgr gameMgr;

    private IDisposable subscription;
    

    public void BuildEnemyStats()
    {
        if (!(subscription == null))
        {
            subscription.Dispose();
        }
        
        monster = GameObject.FindWithTag("Monster").GetComponent<BattleMonster>();
        gameMgr = GameObject.FindWithTag("GameManager").GetComponent<TestGameMgr>();
        
        EnemyInitialHp = Random.Range(hpRange.x, hpRange.y);
        enemyNowHp.Value = EnemyInitialHp;
        
        subscription = enemyNowHp.Subscribe(x => {
            if (enemyNowHp.Value < 0)
            {
                enemyNowHp.Value = 0;
                Debug.Log("エネミーは倒れた！");
                gameMgr.GoNextStage();
                Destroy(this.gameObject);
            }
        });
        Debug.Log($"エネミーHp:{enemyNowHp.Value}");

        EnemyInitialAtk = Random.Range(atkRange.x, atkRange.y);
        enemyNowAtk = EnemyInitialAtk;
        Debug.Log($"エネミーAtk:{EnemyInitialAtk}");
        EnemyInitialDef = Random.Range(defRange.x, defRange.y);
        enemyNowDef = EnemyInitialDef;
        Debug.Log($"エネミーDef:{EnemyInitialDef}");
    }

    public IEnumerator RunEnemyCommand()
    {
        yield return new WaitForSeconds(2);

        enemyNowAtk = EnemyInitialAtk;
        enemyNowDef = EnemyInitialDef;
        
        int enemyCommandSeed = Random.Range(0, 10);

        if (enemyCommandSeed >= 0 && enemyCommandSeed < 6)
        {
            AttackEnemy();
        }
        else if (enemyCommandSeed < 7)
        {
            DefendEnemy();
        }
        else if(enemyCommandSeed < 8)
        {
            AccumulateEnemyAtk();
        }
        else if (enemyCommandSeed < 9)
        {
            RecoverEnemyHp();
        }
        else
        {
            DoNothingEnemy();
        }
        
        yield return new WaitForSeconds(2);
        gameMgr.isPlayerTurn.Value = true;
    }

    private void AttackEnemy()
    {
        Debug.Log("エネミーの攻撃！");
        monster.AttackedMonster(enemyNowAtk);
    }

    private void DefendEnemy()
    {        
        Debug.Log("エネミーの防御！");
        enemyNowDef *= 2;
    }

    private void AccumulateEnemyAtk()
    {
        Debug.Log("エネミーは攻撃を貯めた");
        enemyNowAtk *= 2;
    }

    private void RecoverEnemyHp()
    {
        Debug.Log("エネミーの回復！！");
        enemyNowHp.Value += 10;
    }

    private void DoNothingEnemy()
    {
        Debug.Log("あ！行動をサボった！");
    }

    public void AttackedEnemy(int playerMonsterAtk)
    {
        int damageReceived = (playerMonsterAtk - enemyNowDef > 0) ? playerMonsterAtk - enemyNowDef : 0;
        Debug.Log($"エネミーは{damageReceived}ダメージ受けた！");
        enemyNowHp.Value -= damageReceived;
    }
}
