using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TitleScene����GenerateCharacterScene�ւ̃V�[���J�ڂ̃X�N���v�g
/// �J�ڐ�ɂ���擾����X�N���v�g��GameObject�ɃA�^�b�`����Ă��Ȃ��Ɠ��삵�Ȃ�
/// </summary>
public class Title : MonoBehaviour
{

    public async void Test()
    {
        var sceneA = await SceneLoader.Load<GenerateCharacter>("GenerateCharacter");
        sceneA.SetArguments();
    }

    void Start()
    {

    }


}
