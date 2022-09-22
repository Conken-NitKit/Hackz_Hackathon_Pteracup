using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingTransition : MonoBehaviour
{
    public void OnClicked()
    {
        SceneManager.LoadScene("Ranking");
    }
}
