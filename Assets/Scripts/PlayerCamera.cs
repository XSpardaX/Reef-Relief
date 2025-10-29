using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target; // Player
    public float followSpeed = 5f;
    public float zoomSpeed = 2f;
    public float baseSize = 5f;
    private Camera cam;
    private float targetZoom;

    private Quaternion fixedRotation;

    void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
        fixedRotation = Quaternion.identity; // Keep camera level
    }

    void LateUpdate()
    {
        if (target)
        {
            // Follow player position
            Vector3 desiredPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);
        }

        // Maintain fixed rotation
        transform.rotation = fixedRotation;

        // Smooth zoom
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
    }

    public void ZoomOut(float amount)
    {
        targetZoom += amount;
    }
}
