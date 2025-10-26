using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

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
    }
}

