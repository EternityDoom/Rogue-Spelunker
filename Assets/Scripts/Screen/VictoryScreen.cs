using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] string gameOverScene = null;
    public void EndGame()
    {
        SceneManager.LoadScene(gameOverScene);
    }
}
