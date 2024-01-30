using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float range = 5.0f;
    public float detectionRadius = 1.0f;
    public LayerMask obstacleLayer;
    public float minPauseDuration = 0.5f;
    public float maxPauseDuration = 1.5f;

    public Weapon[] weapons = new Weapon[2];
    private Weapon currentWeapon;

    private Vector3 targetPosition;
    private bool isMoving = true;

    public float targetDetectionRadius = 5.0f;
    public string targetTag = "Target";
    private GameObject currentTarget = null;
    public float stopChaseDistance = 2.0f;

    private void Start()
    {
        SetRandomDestination();

        AdjustBehaviorBasedOnWeapons();

    }

    private void Update()
    {
        if (currentTarget != null)
        {

            SelectWeaponForAttack();

            AdjustBehaviorBasedOnCurrentWeapon();

            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distanceToTarget > targetDetectionRadius)
            {
                currentTarget = null;
                isMoving = true;
                SetRandomDestination();
            }
            else if (distanceToTarget > stopChaseDistance)
            {
                MoveTowardsTarget(currentTarget.transform.position);
            }
            else
            {
                FaceTarget(currentTarget.transform.position);
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

    private void AdjustBehaviorBasedOnWeapons()
    {
        float maxRange = 0f;
        foreach (var weapon in weapons)
        {
            if (weapon != null && weapon.effectiveRange > maxRange)
            {
                maxRange = weapon.effectiveRange;
            }
        }
        stopChaseDistance = maxRange;
    }

    private void SelectWeaponForAttack()
    {
        if (weapons.Length > 0)
        {
            int randomIndex = Random.Range(0, weapons.Length);
            currentWeapon = weapons[randomIndex];
        }
    }

    private void AdjustBehaviorBasedOnCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            stopChaseDistance = currentWeapon.effectiveRange;
        }
    }

}