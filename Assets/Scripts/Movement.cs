using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f; // Degrees per second
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection.y = 1;
        if (Input.GetKey(KeyCode.S))
            moveDirection.y = -1;
        if (Input.GetKey(KeyCode.D))
            moveDirection.x = 1;
        if (Input.GetKey(KeyCode.A))
            moveDirection.x = -1;
    }
    
    void FixedUpdate()
    {
        Vector2 targetVelocity = moveDirection.normalized * moveSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 0.1f);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            // Flip character based on horizontal direction
            if (moveDirection.x < 0 && facingRight)
                Flip();
            else if (moveDirection.x > 0 && !facingRight)
                Flip();

            // Calculate target rotation angle (relative to facing direction)
            float angle = Mathf.Atan2(moveDirection.y, Mathf.Abs(moveDirection.x)) * Mathf.Rad2Deg;

            // Invert rotation if flipped
            if (!facingRight)
                angle = -angle;

            // Smoothly rotate toward target
            float newAngle = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.fixedDeltaTime / 360f);
            rb.MoveRotation(newAngle);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Mirror horizontally
        transform.localScale = scale;
    }
}

