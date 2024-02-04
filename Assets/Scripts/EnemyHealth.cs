using System.ComponentModel;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private AIMovement aiMovement;
    public EnemyDeathCache enemyDeathCache;
    public float totalHealth;

    void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        enemyDeathCache = GetComponent<EnemyDeathCache>();
        UpdateTotalHealth();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            Die();
        }
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
        enemyDeathCache.Death();
    }
}
