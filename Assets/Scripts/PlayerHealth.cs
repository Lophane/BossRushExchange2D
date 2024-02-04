using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public PlayerHealthCount playerHealthCount;

    private void Start()
    {
        GameObject uiObject = GameObject.FindWithTag("UI");
        if (uiObject != null) // Check if the GameObject was found
        {
            playerHealthCount = uiObject.GetComponent<PlayerHealthCount>();
        }
        else
        {
            Debug.LogError("The object with tag 'UI' was not found in the scene.");
        }
    }

    public void ApplyDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log($"Player health now {health}");

        if (playerHealthCount != null)
        {
            playerHealthCount.UpdateHeartUI(health);
        }

        if (health <= 0)
        {
            Debug.Log("Player is dead!");
            GameManager.instance.EndGame();

        }
    }
}
