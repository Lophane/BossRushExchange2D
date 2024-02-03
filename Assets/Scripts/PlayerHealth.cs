using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public PlayerHealthCount playerHealthCount;

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
            // Handle player death here (e.g., triggering a death animation, game over logic, etc.)
            Debug.Log("Player is dead!");
        }
    }
}
