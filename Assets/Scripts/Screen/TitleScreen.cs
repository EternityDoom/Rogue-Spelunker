using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] string startScene = null;
    [SerializeField] string tutorialScene = null;

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void TutorialScene()
    {
        SceneManager.LoadScene(tutorialScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
