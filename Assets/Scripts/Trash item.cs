using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public bool isCollected = false;
    public float zoomOutAmount = 2f;   // How much to zoom out per pickup
    public float speedBoost = 0.5f;      // How much to increase player speed

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var inv = col.GetComponent<PlayerInventory>();
            if (inv && inv.CanCollect())
            {
                inv.CollectTrash();
                isCollected = true;
                Destroy(gameObject);

                // ?? Boost speed
                var move = col.GetComponent<Movement>();
                if (move)
                    move.moveSpeed += speedBoost;

                // ?? Zoom out camera smoothly
                var camFollow = Camera.main.GetComponent<PlayerCamera>();
                if (camFollow)
                    camFollow.ZoomOut(zoomOutAmount);

                FindAnyObjectByType<LevelManager>()?.CheckAllTrashCollected();
            }
        }
    }
}