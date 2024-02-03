using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerHealth playerHealth;
    public int startHealth;

    private bool isGameRunning = false;

    private bool isGamePaused = false;
    public KeyCode pauseKey = KeyCode.Tab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (IsGameRunning() && Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        isGameRunning = true;
        playerHealth.health = startHealth;
        // Reset game state, scores, etc.
        Debug.Log("Game Started");
    }

    public void EndGame()
    {
        isGameRunning = false;
        Debug.Log("Game Ended");
    }

    // Optional: A method to restart the game
    public void RestartGame()
    {
        StartGame(); // Restart game state
        Debug.Log("Game Restarted");
    }

    // Use this to check the game's current state from other scripts
    public bool IsGameRunning()
    {
        return isGameRunning;
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
