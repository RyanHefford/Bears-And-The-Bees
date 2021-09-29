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
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MenuSceneTemp");
    }

    public void StopPause()
    {
        Time.timeScale = 1f;
        GameObject pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        PlayerPrefs.SetInt("Paused", 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
