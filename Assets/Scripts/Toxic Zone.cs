using UnityEngine;

public class ToxicZone : MonoBehaviour
{
    public float drainMultiplier = 2f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            col.GetComponent<OxygenSystem>()?.BoostOxygenDrain(drainMultiplier);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            col.GetComponent<OxygenSystem>()?.BoostOxygenDrain(1f);
    }
}
