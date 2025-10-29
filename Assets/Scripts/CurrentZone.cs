using UnityEngine;

public class CurrentZone : MonoBehaviour
{
    public float pushForce = 2f;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Vector2 pushDirection = transform.right; 

            col.GetComponent<Rigidbody2D>()?.AddForce(pushDirection.normalized * pushForce);
        }
    }
}