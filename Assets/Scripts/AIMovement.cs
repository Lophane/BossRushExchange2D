using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float range = 5.0f;
    public float detectionRadius = 1.0f;
    public LayerMask obstacleLayer;
    public float minPauseDuration = 0.5f;
    public float maxPauseDuration = 1.5f;

    private Vector3 targetPosition;
    private bool isMoving = true;

    public float targetDetectionRadius = 5.0f;
    public string targetTag = "Target";
    private GameObject currentTarget = null;
    public float stopChaseDistance = 2.0f;

    public List<WeaponData> allWeapons;
    public List<WeaponData> equippedWeapons = new List<WeaponData>();
    private WeaponData currentWeapon;
    public GameObject leftArmAttachmentPoint;
    public GameObject rightArmAttachmentPoint;

    public float attackCooldown = 1.0f;
    private float currentAttackCooldown;

    private int health = 10;


    private void Start()
    {
        InitializeWeapons();
        SetRandomDestination();
        currentAttackCooldown = attackCooldown;

    }

    private void Update()
    {
        if (currentTarget != null)
        {

            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distanceToTarget > targetDetectionRadius)
            {
                currentTarget = null;
                isMoving = true;
                currentWeapon = null;
                SetRandomDestination();
            }
            else if (distanceToTarget > stopChaseDistance)
            {

                if (currentWeapon == null)
                {
                    SelectWeaponForAttack();

                    AdjustBehaviorBasedOnCurrentWeapon();
                }

                MoveTowardsTarget(currentTarget.transform.position);
            }
            else
            {
                FaceTarget(currentTarget.transform.position);

                if (currentAttackCooldown <= 0f)
                {
                    AttackWithCurrentWeapon();
                    currentAttackCooldown = attackCooldown; // Reset cooldown

                }
            }
        }
        else
        {
            if (isMoving)
            {
                if (DetectTarget())
                {
                    isMoving = false;
                }
                else if (IsPathBlocked())
                {
                    StartCoroutine(ChangeDirectionAfterDelay());
                }
                else if (HasReachedDestination())
                {
                    StartCoroutine(ChangeDirectionAfterDelay());
                }
                else
                {
                    MoveTowardsTarget(targetPosition);
                }
            }
        }

        if (currentAttackCooldown >= 0.0f) 
        {
            Debug.Log(currentAttackCooldown);
            currentAttackCooldown -= Time.deltaTime; // Decrease cooldown over time
        }

        if (health <= 0)
        {
            //dead
        }


    }


    //wanted a pause between movements, feels more real
    IEnumerator ChangeDirectionAfterDelay()
    {
        isMoving = false;
        float pauseDuration = Random.Range(minPauseDuration, maxPauseDuration);
        yield return new WaitForSeconds(pauseDuration);
        SetRandomDestination();
        isMoving = true;
    }

    //I dislike waypoints, they'll just wander randomly unless we decide we want them to stay near where they spawned
    void SetRandomDestination()
    {
        float randomX = Random.Range(-range, range);
        float randomY = Random.Range(-range, range);
        targetPosition = new Vector3(randomX, randomY, 0) + transform.position;
    }

    //Self explanitory I hope
    void MoveTowardsTarget(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);

        Vector3 direction = (position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    //Do it all again
    bool HasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPosition) < .1f;
    }

    //Staredown.exe
    void FaceTarget(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    //Stops AI from running into walls and obsticles while in wander mode
    bool IsPathBlocked()
    {
        Vector2 direction = targetPosition - transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRadius, direction, detectionRadius, obstacleLayer);
        return hit.collider != null;
    }

    //Function to find something tagged 'Traget' within the detection radius
    bool DetectTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetDetectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag(targetTag) && hit.gameObject != gameObject)
            {
                currentTarget = hit.gameObject;
                return true;
            }
        }
        return false;
    }

    //Adjusts minimum range from target to what the minimum range for the selected weapon is
    private void AdjustBehaviorBasedOnCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            stopChaseDistance = currentWeapon.effectiveRange;
        }
    }

    //Chooses 2 random weapons and equips them to either arm
    private void InitializeWeapons()
    {
        WeaponData weapon1 = ChooseRandomWeapon();
        WeaponData weapon2 = ChooseRandomWeapon(weapon1);

        //Debug.Log(weapon1 + " & " + weapon2);

        EquipWeapon(weapon1, leftArmAttachmentPoint);
        EquipWeapon(weapon2, rightArmAttachmentPoint);
    }

    //choses a random weapon from the list avalable to the enemy (List in prefab)
    private WeaponData ChooseRandomWeapon(WeaponData exclude = null)
    {
        WeaponData chosenWeapon;
        do
        {
            chosenWeapon = allWeapons[Random.Range(0, allWeapons.Count)];
        } while (chosenWeapon == exclude);
        return chosenWeapon;
    }

    //Instantializes the chosen weapon to the correct arm slot
    private void EquipWeapon(WeaponData weaponData, GameObject attachmentPoint)
    {
        if (weaponData != null && weaponData.weaponPrefab != null)
        {
            GameObject weaponObj = Instantiate(weaponData.weaponPrefab, attachmentPoint.transform);
            weaponObj.transform.localPosition = Vector3.zero;
            equippedWeapons.Add(weaponData); // Add the WeaponData to the list
        }
    }

    //chooses an equip weapon to be the main weapon for the next attack
    private void SelectWeaponForAttack()
    {
        if (equippedWeapons != null && equippedWeapons.Count > 0)
        {
            int randomIndex = Random.Range(0, equippedWeapons.Count);
            currentWeapon = equippedWeapons[randomIndex];

            attackCooldown = currentWeapon.attackSpeed; 
            Debug.Log("Weapon selected: " + currentWeapon.weaponName);

        }
    }

    private void AttackWithCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            // attack code

            SelectWeaponForAttack();
            AdjustBehaviorBasedOnCurrentWeapon();
            Debug.Log("attacked, switching to " + currentWeapon);
        }
        else
        {
            Debug.LogWarning("No weapon selected for attack");
        }
    }

}