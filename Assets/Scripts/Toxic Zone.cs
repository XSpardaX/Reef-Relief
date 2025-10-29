using UnityEngine;

public class ToxicZone : MonoBehaviour
{
    [Header("Oxygen Drain Settings")]
    public float drainMultiplier = 2f;

    [Header("Pulse Settings")]
    public float pulseSpeed = 1f;         
    public float pulseAmount = 0.2f;       
    private Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        // Smooth pulsing effect using sine wave
        float scaleOffset = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = baseScale * scaleOffset;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<OxygenSystem>()?.BoostOxygenDrain(drainMultiplier);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<OxygenSystem>()?.BoostOxygenDrain(1f);
        }
    }
}
