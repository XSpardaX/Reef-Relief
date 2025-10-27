using UnityEngine;

public class Surface : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var oxy = col.GetComponent<OxygenSystem>();
            oxy?.RefillOxygen();

            var inv = col.GetComponent<PlayerInventory>();
            if (inv)
            {
                int deposited = inv.DepositTrash();
                FindAnyObjectByType<LevelManager>()?.OnTrashDeposited(deposited);
            }
        }
    }
}
