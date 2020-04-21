using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] string startScene = null;

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
