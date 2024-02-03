using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
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
            Debug.Log("Player is dead!");
            GameManager.instance.EndGame();

        }
    }
}
