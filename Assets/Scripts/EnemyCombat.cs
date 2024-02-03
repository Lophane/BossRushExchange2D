using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public WeaponData currentWeapon;
    private Animator animator;
    private BoxCollider2D hitbox;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InitializeWeapon();
    }

    private void InitializeWeapon()
    {
        if (currentWeapon == null) return;

        // Assuming the weapon prefab includes a Sprite and Collider setup
        GameObject weaponInstance = Instantiate(currentWeapon.weaponPrefab, transform);

        // Set up hitbox - assuming 2D game
        hitbox = weaponInstance.AddComponent<BoxCollider2D>();
        hitbox.size = currentWeapon.hitboxSize;
        hitbox.offset = currentWeapon.hitboxOffset;
        hitbox.isTrigger = true; // Set as trigger if you want it to not physically block entities

        // Disable the hitbox initially if it should only be enabled during attacks
        hitbox.enabled = false;
    }

    public void Attack()
    {
        if (animator == null || currentWeapon == null) return;

        // Trigger animation
        animator.Play(currentWeapon.attackAnimation.name);

        // Enable hitbox at the start of the animation via Animation Event or Coroutine based on animation timing
        hitbox.enabled = true;

        // Consider disabling the hitbox after the attack animation completes
        // This could be done using an Animation Event at the end of the attack animation clip
    }

    // Method to disable hitbox - can be called at the end of an attack animation via Animation Event
    public void DisableHitbox()
    {
        if (hitbox != null) hitbox.enabled = false;
    }

    // Example method to be called by hitbox trigger event to apply damage
    public void ApplyDamage(GameObject target)
    {
        // Implement damage application logic here
        Debug.Log($"Applying {currentWeapon.damage} damage to {target.name}");
    }
}