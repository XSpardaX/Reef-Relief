using UnityEngine;

public class CurrentZone : MonoBehaviour
{
    public Vector2 pushDirection = Vector2.right;
    public float pushForce = 2f;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            col.GetComponent<Rigidbody2D>().AddForce(pushDirection.normalized * pushForce);
    }
}
