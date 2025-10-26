using UnityEngine;

public class Trashitem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerInventory inv = col.GetComponent<PlayerInventory>();
            if (inv && inv.CanCollect())
            {
                inv.CollectTrash();
                Destroy(gameObject);
            }
        }
    }
}
