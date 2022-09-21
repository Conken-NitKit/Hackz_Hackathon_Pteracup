using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

/// <summary>
/// GenerateCharacter.csから渡される値をもらうスクリプト
/// </summary>
public class Main : MonoBehaviour
{

    private byte[] bytes;

    [SerializeField] 
    private BattleMonster battleMonster;

    [SerializeField]
    private MonsterBack monsterBack;

    [SerializeField] 
    private Text userNameText;

    [SerializeField]
    private BattleGameManager battleGameManager;

    private string charaName;

    /// <summary>
    /// 値をもらう関数
    /// </summary>
    /// <param name="colorCode"></param>
    /// <param name="characterName"></param>
    public void SetArguments(string colorCode,string characterName, Monster monster)
    {
        battleMonster.DecideMonsterStatus(monster);
        monsterBack.SetMonsterBackColor(colorCode);
        userNameText.text = characterName;
        battleGameManager.GoNextStage();
        charaName = characterName;
    }
    public async void PassMainToGameOver()
    {
        await Task.Delay(3000);
        var sceneB = await SceneLoader.Load<GameOver>("GameOver");
        sceneB.SetArguments(battleGameManager.enemyKillNum ,charaName);
    }
}
