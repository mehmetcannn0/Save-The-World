using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public GameObject[] buttons;
    public void OpenTutorialScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialScene");
    }
    public void OpenGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
