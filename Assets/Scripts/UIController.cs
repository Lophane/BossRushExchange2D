using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public GameObject gameOverScreen;
    public GameObject pauseScreen;

    public void SetGameOverScreen(bool isOn)
    {
        gameOverScreen.SetActive(isOn);
    }

    public void SetPauseScreen(bool isOn)
    {
        pauseScreen.SetActive(isOn);
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene("Test");
    }

    public void QuitGameButton()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }

    public void Resume()
    {
        GameManager.instance.UnpauseGame();
    }

    public void RestartButton()
    {
        GameManager.instance.StartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
