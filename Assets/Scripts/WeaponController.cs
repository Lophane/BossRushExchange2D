using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData primaryWeapon;
    public WeaponData secondaryWeapon;

    private Animator animator;
    private BoxCollider2D primaryHitbox;
    private BoxCollider2D secondaryHitbox;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InitializeWeapon(primaryWeapon, ref primaryHitbox);
        InitializeWeapon(secondaryWeapon, ref secondaryHitbox);
    }

    private void InitializeWeapon(WeaponData weaponData, ref BoxCollider2D hitbox)
    {
        if (weaponData == null) return;

        GameObject weaponInstance = Instantiate(weaponData.weaponPrefab, transform);

        hitbox = weaponInstance.AddComponent<BoxCollider2D>();
        hitbox.size = weaponData.hitboxSize;
        hitbox.offset = weaponData.hitboxOffset;
        hitbox.isTrigger = true; // Set as trigger
        hitbox.enabled = false; // Initially disabled
    }

    public void AttackWithPrimary()
    {
        Attack(primaryWeapon, primaryHitbox);
    }

    public void AttackWithSecondary()
    {
        Attack(secondaryWeapon, secondaryHitbox);
    }

    private void Attack(WeaponData weaponData, BoxCollider2D hitbox)
    {
        if (animator == null || weaponData == null) return;

        // Assuming you have a way to differentiate animations for primary and secondary weapons
        animator.Play(weaponData.attackAnimation.name);

        hitbox.enabled = true;
        // Disable the hitbox after the attack using an animation event or a timed coroutine
    }

    // Call this method to disable the hitbox, linked to an animation event or the end of a coroutine
    public void DisableHitbox(BoxCollider2D hitbox)
    {
        if (hitbox != null) hitbox.enabled = false;
    }
}
