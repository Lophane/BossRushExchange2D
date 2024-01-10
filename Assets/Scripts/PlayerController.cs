using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float sprintMultiplier = 2.0f;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode dashKey = KeyCode.Space;
    public float dashDistance = 10.0f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1.0f;
    public string wallTag = "Wall"; // Tag for identifying walls

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private float lastDashTime = -Mathf.Infinity;
    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKey(sprintKey) && !isDashing)
        {
            moveVelocity = moveInput * moveSpeed * sprintMultiplier;
        }
        else if (!isDashing)
        {
            moveVelocity = moveInput * moveSpeed;
        }

        if (Input.GetKeyDown(dashKey) && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            StartCoroutine(Dash());
        }

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    IEnumerator Dash()
    {
        isDashing = true;
        Vector2 dashVelocity = moveInput * (dashDistance / dashDuration);
        float dashEndTime = Time.time + dashDuration;

        while (Time.time < dashEndTime)
        {
            Vector2 newPosition = rb.position + dashVelocity * Time.deltaTime;

            // Raycast to check for wall collision
            RaycastHit2D hit = Physics2D.Raycast(rb.position, dashVelocity, dashVelocity.magnitude * Time.deltaTime);
            if (hit.collider != null && hit.collider.CompareTag(wallTag))
            {
                // If a wall is hit, stop the dash
                break;
            }

            rb.MovePosition(newPosition);
            yield return null;
        }

        isDashing = false;
        lastDashTime = Time.time;
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
    }
}
