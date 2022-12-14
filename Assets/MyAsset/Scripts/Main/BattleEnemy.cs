using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;

/// <summary>
/// 戦闘時のエネミーのステータス、コマンドの管理クラス
/// </summary>
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

    public bool dieEnemy{ get; private set; }

    [SerializeField]
    private BattleMonster monster;
    
    [SerializeField] 
    private BattleGameManager gameMgr;

    private IDisposable subscription;
    
    private SceneAnimation sceneAnimation;
    
    [SerializeField]
    private Text gameText;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField] 
    private Sprite[] enemySprites;

    /// <summary>
    /// エネミーのステータスを生成するメソッド
    /// </summary>
    public void BuildEnemyStats()
    {
        if (!(subscription == null))
        {
            subscription.Dispose();
        }

        dieEnemy = false;
        
        gameText = GameObject.FindWithTag("GameText").GetComponent<Text>();
        
        monster = GameObject.FindWithTag("Monster").GetComponent<BattleMonster>();
        gameMgr = GameObject.FindWithTag("GameManager").GetComponent<BattleGameManager>();
        sceneAnimation = GameObject.FindWithTag("GameManager").GetComponent<SceneAnimation>();
        
        EnemyInitialHp = Random.Range(hpRange.x, hpRange.y);
        enemyNowHp.Value = EnemyInitialHp;
        
        subscription = enemyNowHp.Subscribe(x => {
            if (enemyNowHp.Value < 0)
            {
                dieEnemy = true; 
                enemyNowHp.Value = 0;
                gameText.text = "エネミーは倒れた！";
                this.transform.DOMoveY(-5f,1f).SetRelative(true);
                this.gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
                sceneAnimation.RiseCurtain(); 
                sceneAnimation.goNextStage = true;
                gameMgr.GoNextStage();
                //Destroy(this.gameObject);
            }
        });
        Debug.Log($"エネミーHp:{enemyNowHp.Value}");

        EnemyInitialAtk = Random.Range(atkRange.x, atkRange.y);
        enemyNowAtk = EnemyInitialAtk;
        Debug.Log($"エネミーAtk:{EnemyInitialAtk}");
        EnemyInitialDef = Random.Range(defRange.x, defRange.y);
        enemyNowDef = EnemyInitialDef;
        Debug.Log($"エネミーDef:{EnemyInitialDef}");

        if (EnemyInitialHp + EnemyInitialAtk + EnemyInitialDef < 40)
        {
            spriteRenderer.sprite = enemySprites[0];
        }
        else if (EnemyInitialHp + EnemyInitialAtk + EnemyInitialDef < 55)
        {
            spriteRenderer.sprite = enemySprites[1];
        }
        else
        {
            spriteRenderer.sprite = enemySprites[2];
        }

        this.transform.DOMoveX(-4f, 1).SetDelay(6f).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// エネミーのコマンド実行するクラス
    /// ランダム
    /// </summary>
    /// <returns></returns>
    public IEnumerator RunEnemyCommand()
    {
        yield return new WaitForSeconds(2);

        enemyNowAtk = EnemyInitialAtk;
        enemyNowDef = EnemyInitialDef;
        
        int enemyCommandSeed = Random.Range(0, 10);

        if (!dieEnemy)
        {
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
        }

        yield return new WaitForSeconds(2);
        gameMgr.isPlayerTurn.Value = true;
    }

    /// <summary>
    /// エネミーの攻撃メソッド
    /// </summary>
    private void AttackEnemy()
    {
        gameText.text = "エネミーの攻撃！";
        this.transform.DOMoveX(0.5f,0.2f).SetRelative(true).SetLoops(2,LoopType.Yoyo);
        monster.AttackedMonster(enemyNowAtk);
    }

    /// <summary>
    /// エネミーの防御メソッド
    /// </summary>
    private void DefendEnemy()
    {        
        gameText.text = "エネミーの防御！";
        this.transform.DOScale(this.transform.localScale * 1.2f, 0.2f).SetLoops(2,LoopType.Yoyo);
        enemyNowDef *= 2;
    }

    /// <summary>
    /// エネミーの力を貯めるメソッド
    /// </summary>
    private void AccumulateEnemyAtk()
    {
        gameText.text = "エネミーは攻撃を貯めた！";
        this.transform.DOScale(this.transform.localScale * 0.7f, 0.5f).SetLoops(2,LoopType.Yoyo);
        enemyNowAtk *= 2;
    }

    /// <summary>
    /// エネミーの回復メソッド
    /// </summary>
    private void RecoverEnemyHp()
    {
        gameText.text = "エネミーの回復！！";
        this.transform.DOMoveY(0.5f,0.05f).SetRelative(true).SetLoops(10,LoopType.Yoyo);
        this.transform.DOScale(this.transform.localScale * 1.2f, 0.05f).SetLoops(10,LoopType.Yoyo);
        enemyNowHp.Value += 10;
    }

    /// <summary>
    /// エネミーが何にも行動しないメソッド
    /// </summary>
    private void DoNothingEnemy()
    {
        gameText.text = "あ！行動をサボった！";
        this.transform.DORotate(new Vector3(0, 0, -90), 0.1f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
    }

    /// <summary>
    /// エネミーが攻撃を受ける時、このメソッドを呼び出して、HPを減らす
    /// </summary>
    /// <param name="playerMonsterAtk"></param>
    public void AttackedEnemy(int playerMonsterAtk)
    {
        int damageReceived = (playerMonsterAtk - enemyNowDef > 0) ? playerMonsterAtk - enemyNowDef : 0;
        gameText.text = $"エネミーは{damageReceived}ダメージ受けた！";
        this.transform.DOMoveX(-0.3f, 1f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
        this.transform.DORotate(new Vector3(0, 0, -10), 0.1f).SetRelative(true).SetLoops(10, LoopType.Yoyo);
        enemyNowHp.Value -= damageReceived;
    }
}
