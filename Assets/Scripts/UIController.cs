using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    private bool isGamePaused = false;
    public KeyCode pauseKey = KeyCode.Tab;

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Test");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        Debug.Log("Paused");
        isGamePaused = true;
        Time.timeScale = 0;
    }
    
    public void UnpauseGame()
    {
        Debug.Log("Unpaused");
        isGamePaused = false;
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        if (isGamePaused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

}
