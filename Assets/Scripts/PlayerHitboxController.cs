using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxController : MonoBehaviour
{
    private BoxCollider2D hitbox0;
    private BoxCollider2D hitbox1;

    public PlayerWeaponController pWC;
    private EnemyHealth enemyHealth;


    private void Start()
    {

        //pWC = GetComponent<PlayerWeaponController>();

        hitbox0 = gameObject.AddComponent<BoxCollider2D>();
        hitbox1 = gameObject.AddComponent<BoxCollider2D>();
        hitbox0.isTrigger = true;
        //hitbox0.tag = "Hitbox0";
        hitbox1.isTrigger = true;
        //hitbox1.tag = "Hitbox1";

        //Debug.Log(pWC.equippedWeapons[0]);
        //Debug.Log(pWC.equippedWeapons[1]);
        UpdateHitbox(0);
        UpdateHitbox(1);

        DisableHitbox(0);
        DisableHitbox(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = other.GetComponent<EnemyHealth>();

            if (pWC.lastAttack0)
            {
                enemyHealth.TakeDamage(pWC.equippedWeapons[0].damage);
            }
            else
                enemyHealth.TakeDamage(pWC.equippedWeapons[1].damage);

        }

    }

    public void DisableHitbox(int arm)
    {
        if (arm == 0)
            hitbox0.enabled = false;
        else
            hitbox1.enabled = false;
    }

    public void EnableHitbox(int arm)
    {
        if (arm == 0)
            hitbox0.enabled = true;
        else
            hitbox1.enabled = true;
    }

    public void UpdateHitbox(int arm)
    {
        //Debug.Log("Updating arm:" + arm);
        if (pWC.equippedWeapons[0] == null || hitbox0 == null || pWC.equippedWeapons[1] == null || hitbox1 == null)
        {
            //Debug.Log(pWC.equippedWeapons[0] + " " + hitbox0 + " " + pWC.equippedWeapons[1] + " " + hitbox1 + " ");
            return;
        }
            

        if (arm == 0)
        {
            //Debug.Log("Updating arm 0");
            hitbox0.size = pWC.equippedWeapons[0].hitboxSize;
            Vector2 currentOffset = pWC.equippedWeapons[0].hitboxOffset;

            currentOffset.x = -currentOffset.x;

            hitbox0.offset = currentOffset;
        }
        else
        {
            //Debug.Log("Updating arm 1");
            hitbox1.size = pWC.equippedWeapons[1].hitboxSize;
            hitbox1.offset = pWC.equippedWeapons[1].hitboxOffset;
        }
    }


}
