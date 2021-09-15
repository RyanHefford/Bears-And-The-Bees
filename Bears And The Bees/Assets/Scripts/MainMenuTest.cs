using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuTest : MonoBehaviour
{

    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelMakingAlgorithm");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlayScene");
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuSceneTemp");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
