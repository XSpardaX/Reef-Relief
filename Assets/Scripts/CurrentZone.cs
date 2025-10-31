using UnityEngine;

public class CurrentZone : MonoBehaviour
{
    public float pushForce = 2f;

    public float bobHeight = 0.25f;
    public float bobSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Vector2 pushDirection = transform.right; 

            col.GetComponent<Rigidbody2D>()?.AddForce(pushDirection.normalized * pushForce);
        }
    }
}