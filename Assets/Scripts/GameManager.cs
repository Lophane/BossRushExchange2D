using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerHealth playerHealth;
    public PlayerController playerController;
    public UIController uiController;
    public int startHealth;

    public bool isGameRunning = true;

    public bool isGamePaused = false;
    public KeyCode pauseKey = KeyCode.Tab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        StartGame();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();
        
        if (uiController == null)
            uiController = FindObjectOfType<UIController>();

    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void Update()
    {
        if (isGameRunning && Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        isGameRunning = true;
        playerHealth.health = startHealth;
        // Reset game state, scores, etc.
        UnpauseGame();
        Debug.Log("Game Started");
        
    }

    public void EndGame()
    {
        isGameRunning = false;
        PauseGame();
        uiController.SetPauseScreen(false);
        uiController.SetGameOverScreen(true);
        Debug.Log("Game Ended");
    }

    // Optional: A method to restart the game
    public void RestartGame()
    {
        StartGame();
        Debug.Log("Game Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Use this to check the game's current state from other scripts
    public bool IsGameRunning()
    {
        return isGameRunning;
    }

    public void PauseGame()
    {
        if (isGamePaused == false)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            uiController.SetPauseScreen(true);
        }
    }

    public void UnpauseGame()
    {
        if (isGamePaused == true)
        {
            isGamePaused = false;
            Time.timeScale = 1;
            uiController.SetPauseScreen(false);
        }
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
