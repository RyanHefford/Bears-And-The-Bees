using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LoadGame()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<PlayMenuMusic>().StopMusic();

        if (!PlayerPrefs.GetString("DifficultyText").Equals("Easy") ||
            !PlayerPrefs.GetString("DifficultyText").Equals("Medium") ||
            !PlayerPrefs.GetString("DifficultyText").Equals("Hard"))
        {
            PlayerPrefs.SetInt("Difficulty", 2);
            PlayerPrefs.SetString("DifficultyText", "Easy");
            PlayerPrefs.SetInt("EnemyAttack", 1);
            PlayerPrefs.SetFloat("SecTillChase", 1.5f);
            PlayerPrefs.SetFloat("AttackRange", 2.5f);
            PlayerPrefs.SetFloat("EnemySpeed", 4.0f);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelMakingAlgorithm");
    }

    public void LoadHowToPlay()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<PlayMenuMusic>().PlayMusic();
        SceneManager.LoadScene("HowToPlayScene");
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<PlayMenuMusic>().PlayMusic();
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

    public void LoadSettingsScene()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<PlayMenuMusic>().PlayMusic();
        SceneManager.LoadScene("SettingsScene");
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }

    public void EasyDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        PlayerPrefs.SetString("DifficultyText", "Easy");
        PlayerPrefs.SetInt("EnemyAttack", 1);
        PlayerPrefs.SetFloat("SecTillChase", 1.5f);
        PlayerPrefs.SetFloat("AttackCD", 2.0f);
        PlayerPrefs.SetFloat("AttackRange", 2.5f);
        PlayerPrefs.SetFloat("EnemySpeed", 4.0f);
    }

    public void MediumDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 4);
        PlayerPrefs.SetString("DifficultyText", "Medium");
        PlayerPrefs.SetInt("EnemyAttack", 2);
        PlayerPrefs.SetFloat("SecTillChase", 1.5f);
        PlayerPrefs.SetFloat("AttackCD", 1.5f);
        PlayerPrefs.SetFloat("AttackRange", 3.0f);
        PlayerPrefs.SetFloat("EnemySpeed", 5.0f);
    }

    public void HardDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 6);
        PlayerPrefs.SetString("DifficultyText", "Hard");
        PlayerPrefs.SetInt("EnemyAttack", 3);
        PlayerPrefs.SetFloat("SecTillChase", 0.5f);
        PlayerPrefs.SetFloat("AttackCD", 1.0f);
        PlayerPrefs.SetFloat("AttackRange", 3.5f);
        PlayerPrefs.SetFloat("EnemySpeed", 6.0f);
    }
}
