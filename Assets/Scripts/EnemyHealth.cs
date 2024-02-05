using System.ComponentModel;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private AIMovement aiMovement;
    public EnemyDeathCache enemyDeathCache;
    public NextAreaZone nextAreaZone;
    //public PlayerWeaponController playerWeaponController;
    public float totalHealth;
    public bool tutorialMob;
    public bool boss = false;



    void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        enemyDeathCache = GetComponent<EnemyDeathCache>();
        nextAreaZone = GameObject.FindWithTag("EndZone").GetComponent<NextAreaZone>();
        UpdateTotalHealth();
    }

    private void Update()
    {

        if (tutorialMob)
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
            totalHealth = totalHealth * aiMovement.magnitude;
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
            if(boss)
                nextAreaZone.bossDead = true;
        }
    }

    void Die()
    {
        enemyDeathCache.Death();
    }

    

}
