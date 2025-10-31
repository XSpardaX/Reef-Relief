using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public bool isCollected = false;
    public float zoomOutAmount = 2f;   
    public float speedBoost = 0.5f;

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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var inv = col.GetComponent<PlayerInventory>();
            if (inv && inv.CanCollect())
            {
                inv.CollectTrash();
                isCollected = true;
                this.gameObject.SetActive(false);

                var move = col.GetComponent<Movement>();
                if (move)
                    move.moveSpeed += speedBoost;

                var camFollow = Camera.main.GetComponent<PlayerCamera>();
                if (camFollow)
                    camFollow.ZoomOut(zoomOutAmount);
            }
        }
    }
}