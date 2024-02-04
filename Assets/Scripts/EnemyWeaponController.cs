using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    private AIMovement aiMovement;
    private Animator animator;
    public WeaponData currentWeapon;
    public EnemyHitboxController eHC;
    public int magnitude;

    private void Start()
    {
        aiMovement = GetComponentInParent<AIMovement>();
        animator = GetComponent<Animator>();
        
        magnitude = aiMovement.magnitude;

    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        eHC.UpdateHitbox();
    }

    
    public void Attack()
    {
        if (currentWeapon == null) return;

        //animator.Play(currentWeapon.attackAnimation.name);
        eHC.Invoke("EnableHitbox", currentWeapon.hitboxStart);
        eHC.Invoke("DisableHitbox", currentWeapon.hitboxEnd);

        aiMovement.AdjustBehaviorBasedOnCurrentWeapon();
    }

    




}
