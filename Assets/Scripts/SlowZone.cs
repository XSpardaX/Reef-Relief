using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowMultiplier = 0.5f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            col.GetComponent<Movement>().moveSpeed *= slowMultiplier;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            col.GetComponent<Movement>().moveSpeed /= slowMultiplier;
    }
}
