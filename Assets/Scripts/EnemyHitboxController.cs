using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxController : MonoBehaviour
{

    public BoxCollider2D hitbox;
    public EnemyWeaponController eWC;
    public AIMovement aiMovement;

    public void Start()
    {
        hitbox = gameObject.AddComponent<BoxCollider2D>();
        hitbox.isTrigger = true;
        hitbox.enabled = false;
    }

    public void UpdateHitbox()
    {
        if (eWC.currentWeapon == null || hitbox == null) return;

        int weaponIndex = aiMovement.equippedWeapons.IndexOf(eWC.currentWeapon);

        //Debug.Log(weaponIndex);

        hitbox.size = eWC.currentWeapon.enemyHitboxSize;

        if (weaponIndex == 1)
        {

            Vector2 currentOffset = eWC.currentWeapon.enemyHitboxOffset;


            currentOffset.y = -currentOffset.y;


            hitbox.offset = currentOffset;
        }
        else
            hitbox.offset = eWC.currentWeapon.enemyHitboxOffset;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
        aiMovement.SelectWeaponForAttack();
        //eWC.EquipWeapon(eWC.currentWeapon);
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && this.enabled)
        {
            //Debug.Log("Hit!");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                //Debug.Log("Damage!");
                playerHealth.ApplyDamage((eWC.currentWeapon.damage) * eWC.magnitude);

            }
        }
    }
}
