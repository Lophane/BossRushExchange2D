using System.ComponentModel;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private AIMovement aiMovement;
    public float totalHealth;

    void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        UpdateTotalHealth();
    }

    void UpdateTotalHealth()
    {
        totalHealth = 0;

        foreach (var weapon in aiMovement.equippedWeapons)
        {
            //Debug.Log("adding " + weapon.armHealth + " health to enemy");
            totalHealth += weapon.armHealth;
        }

        //Debug.Log("Total Health: " + totalHealth);
    }
    public void TakeDamage(float damage)
    {
        totalHealth -= damage;
        //Debug.Log(gameObject.name + " took " + damage + " damage. Remaining health: " + totalHealth);

        if (totalHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died.");
    }
}
