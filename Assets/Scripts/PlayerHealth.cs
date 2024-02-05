using UnityEngine;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public PlayerHealthCount playerHealthCount;
    public PlayerWeaponController PlayerWeaponController;
    public PlayerStats stats;

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

        health = GameManager.instance.pHealth;

    }

    public void ApplyDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log($"Player health now {health}");

        if (playerHealthCount != null)
        {
            playerHealthCount.UpdateHeartUI(health);
            GameManager.instance.pHealth = health;
        }

        if (health <= 0)
        {
            Debug.Log("Player is dead!");
            GameManager.instance.EndGame();

        }
    }

    public void CalculateHealth(int newArm, int arm2)
    {
        maxHealth = newArm + arm2; 

        health = Math.Min(health + newArm, maxHealth);

        playerHealthCount.UpdateHeartUI(health);
    }

   
}
