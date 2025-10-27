using UnityEngine;

public class DropOffZone : MonoBehaviour
{
    public int score = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerInventory inv = col.GetComponent<PlayerInventory>();
            if (inv)
            {
                int deposited = inv.DepositTrash();
                score += deposited;
                Debug.Log("Deposited " + deposited + " trash. Score: " + score);
            }
        }
    }
}