using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float lowOxygenSpeedMultiplier = 0.5f; 
    public float rotationSpeed = 720f;

    public Animator animator; 
    public OxygenSystem oxygenSystem; 

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

        bool isMoving = moveDirection.sqrMagnitude > 0.01f;
        animator.SetBool("isSwimming", isMoving);

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        animator.speed = isSprinting ? 1.5f : 1f;
    }

    void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            currentSpeed *= sprintMultiplier;

        if (oxygenSystem != null && oxygenSystem.CurrentOxygen <= 0)
            currentSpeed *= lowOxygenSpeedMultiplier;

        Vector2 targetVelocity = moveDirection.normalized * currentSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 0.1f);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            if (moveDirection.x < 0 && facingRight)
                Flip();
            else if (moveDirection.x > 0 && !facingRight)
                Flip();

            float angle = Mathf.Atan2(moveDirection.y, Mathf.Abs(moveDirection.x)) * Mathf.Rad2Deg;
            if (!facingRight)
                angle = -angle;
            float newAngle = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.fixedDeltaTime / 360f);
            rb.MoveRotation(newAngle);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}