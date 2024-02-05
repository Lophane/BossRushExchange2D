using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeathCache : MonoBehaviour
{
    private AIMovement aiMovement;
    private EnemyWeaponController enemyWeaponController;
    private Collider2D enemyCollider;
    public List<WeaponData> cachedEquippedWeapons = new List<WeaponData>();
    public EnemyHealth enemyHealth;

    void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        enemyCollider = GetComponent<Collider2D>();
        enemyWeaponController = GetComponent<EnemyWeaponController>();
        enemyHealth = GetComponent<EnemyHealth>();

        if (aiMovement != null)
        {
            cachedEquippedWeapons = new List<WeaponData>(aiMovement.equippedWeapons);
        }
    }

    public void Death()
    {
         HandleDeath();
        //Debug.Log("I'm Dead AF");
    }

    void HandleDeath()
    {
        if (aiMovement != null)
        {
            aiMovement.enabled = false;
        }

        if (enemyCollider != null)
        {
            enemyCollider.isTrigger = true;
        }
        if (enemyWeaponController != null)
        {
            enemyWeaponController.enabled = false;
            Debug.Log("enemyWeaponController is disabled");
        }

        this.gameObject.tag = "Corpse";

        if (!enemyHealth.tutorialMob)
            StartCoroutine(DestroyAfterCooldown(20.0f));
    }

    IEnumerator DestroyAfterCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        Destroy(gameObject);
    }

    public List<WeaponData> GetCachedWeapons()
    {
        return cachedEquippedWeapons;
    }

}
