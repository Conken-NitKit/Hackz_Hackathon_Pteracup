using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// �V�[�������[�h����
/// ���[�h��V�[���̃R���|�[�l���g���擾�ł���̂Ńp�����[�^�[��n����
/// �擾�ł���R���|�[�l���g�̓��[�h��V�[���̃��[�g�K�w��GameObject�ɐݒu����Ă������
/// �w��R���|�[�l���g���擾�ł���̂�Awake�̌�AStart�̑O�̃^�C�~���O
/// </summary>
public static class SceneLoader
{
    /// <summary>
    /// ���[�h����
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    /// <param name="mode">�V�[�����[�h���[�h</param>
    /// <returns>���[�h��V�[���̃R���|�[�l���g</returns>
    public static UniTask<TComponent> Load<TComponent>(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        where TComponent : Component
    {
        var tcs = new UniTaskCompletionSource<TComponent>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName, mode);
        return tcs.Task;

        void OnSceneLoaded(Scene scene, LoadSceneMode _mode)
        {
            // ��x�C�x���g���󂯂���s�v�Ȃ̂ŉ���
            SceneManager.sceneLoaded -= OnSceneLoaded;

            // ���[�h�����V�[���̃��[�g�K�w��GameObject����w��R���|�[�l���g��1�擾����
            var target = GetFirstComponent<TComponent>(scene.GetRootGameObjects());

            tcs.TrySetResult(target);
        }
    }

    /// <summary>
    /// ���[�h����
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    /// <param name="mode">�V�[�����[�h���[�h</param>
    public static void Load(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(sceneName, mode);
    }

    /// <summary>
    /// �񓯊����[�h����
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    /// <param name="mode">�V�[�����[�h���[�h</param>
    /// <returns>���[�h��V�[���̃R���|�[�l���g</returns>
    public static async UniTask<TComponent> LoadAsync<TComponent>(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        where TComponent : Component
    {
        await SceneManager.LoadSceneAsync(sceneName, mode);

        Scene scene = SceneManager.GetSceneByName(sceneName);

        return GetFirstComponent<TComponent>(scene.GetRootGameObjects());
    }

    /// <summary>
    /// GameObject�z�񂩂�w��̃R���|�[�l���g����擾����
    /// </summary>
    /// <typeparam name="TComponent">�擾�ΏۃR���|�[�l���g</typeparam>
    /// <param name="gameObjects">GameObject�z��</param>
    /// <returns>�ΏۃR���|�[�l���g</returns>
    private static TComponent GetFirstComponent<TComponent>(GameObject[] gameObjects)
        where TComponent : Component
    {
        TComponent target = null;
        foreach (GameObject go in gameObjects)
        {
            target = go.GetComponent<TComponent>();
            if (target != null) break;
        }
        return target;
    }
}