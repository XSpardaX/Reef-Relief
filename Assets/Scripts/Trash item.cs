using UnityEngine;

public class Trashitem : MonoBehaviour
{
    public bool isCollected = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var inv = col.GetComponent<PlayerInventory>();
            if (inv && inv.CanCollect())
            {
                inv.CollectTrash();
                isCollected = true; // ? Mark as collected
                Destroy(gameObject);

                FindAnyObjectByType<LevelManager>()?.CheckAllTrashCollected();
            }
        }
    }
}
