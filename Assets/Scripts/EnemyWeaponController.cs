using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    private AIMovement aiMovement;
    private Animator animator;
    public WeaponData currentWeapon;
    private BoxCollider2D hitbox;

    private void Start()
    {
        aiMovement = GetComponentInParent<AIMovement>();
        animator = GetComponent<Animator>();
        hitbox = gameObject.AddComponent<BoxCollider2D>();
        hitbox.isTrigger = true;
        hitbox.enabled = false;
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        UpdateHitbox();
    }

    private void UpdateHitbox()
    {
        if (currentWeapon == null || hitbox == null) return;

        hitbox.size = currentWeapon.hitboxSize;
        hitbox.offset = currentWeapon.hitboxOffset;
    }
    public void Attack()
    {
        if (currentWeapon == null) return;

        //animator.Play(currentWeapon.attackAnimation.name);
        Invoke("EnableHitbox", currentWeapon.hitboxStart);
        Invoke("DisableHitbox", currentWeapon.hitboxEnd);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit!");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Damage!");
                playerHealth.ApplyDamage(currentWeapon.damage);

            }
        }
    }




}
