using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [Header("Slow Settings")]
    public float slowMultiplier = 0.5f;

    [Header("Movement Settings")]
    public bool moveHorizontally = true;  // toggle left-right or up-down motion
    public float moveSpeed = 0.5f;        // how fast it moves
    public float moveDistance = 1f;       // how far it moves from its start position

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Smooth movement using sine wave
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        if (moveHorizontally)
            transform.position = new Vector3(startPos.x + offset, startPos.y, startPos.z);
        else
            transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var move = col.GetComponent<Movement>();
            if (move != null)
                move.moveSpeed *= slowMultiplier;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var move = col.GetComponent<Movement>();
            if (move != null)
                move.moveSpeed /= slowMultiplier;
        }
    }
}
