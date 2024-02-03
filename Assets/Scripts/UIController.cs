using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public void StartGameButton()
    {
        SceneManager.LoadScene("Test");
    }

    public void QuitGameButton()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

}
