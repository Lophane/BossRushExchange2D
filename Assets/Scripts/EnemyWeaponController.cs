using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    private AIMovement aiMovement;
    private Animator animator;
    public WeaponData currentWeapon;
    private BoxCollider2D hitbox;
    public int magnitude;

    private void Start()
    {
        aiMovement = GetComponentInParent<AIMovement>();
        animator = GetComponent<Animator>();
        hitbox = gameObject.AddComponent<BoxCollider2D>();
        hitbox.isTrigger = true;
        hitbox.enabled = false;
        magnitude = aiMovement.magnitude;

    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        UpdateHitbox();
    }

    private void UpdateHitbox()
    {
        if (currentWeapon == null || hitbox == null) return;

        int weaponIndex = aiMovement.equippedWeapons.IndexOf(currentWeapon);

        //Debug.Log(weaponIndex);

        hitbox.size = currentWeapon.enemyHitboxSize;

        if (weaponIndex == 1)
        {
            
            Vector2 currentOffset = currentWeapon.enemyHitboxOffset;

            
            currentOffset.y = -currentOffset.y;

            
            hitbox.offset = currentOffset;
        }
        else
            hitbox.offset = currentWeapon.enemyHitboxOffset;
    }
    public void Attack()
    {
        if (currentWeapon == null) return;

        //animator.Play(currentWeapon.attackAnimation.name);
        Invoke("EnableHitbox", currentWeapon.hitboxStart);
        Invoke("DisableHitbox", currentWeapon.hitboxEnd);

        aiMovement.AdjustBehaviorBasedOnCurrentWeapon();
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
        aiMovement.SelectWeaponForAttack();
        EquipWeapon(currentWeapon);
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
                playerHealth.ApplyDamage((currentWeapon.damage) * magnitude);

            }
        }
    }




}
