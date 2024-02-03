using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    public void ApplyDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log($"Player health now {health}");

        if (health <= 0)
        {
            // Handle player death here (e.g., triggering a death animation, game over logic, etc.)
            Debug.Log("Player is dead!");
        }
    }
}
