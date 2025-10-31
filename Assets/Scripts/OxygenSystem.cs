using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OxygenSystem : MonoBehaviour
{
    public float maxOxygen;
    public float currentOxygen;
    public float baseDrainRate = 1f;
    public float depthMultiplier = 0.05f;
    public float fastDrainMultiplier = 1f;
    public Slider oxygenBar;
    public float CurrentOxygen => currentOxygen;
    private LevelManager levelManager;

    void Start()
    {
        maxOxygen = 100f;
        currentOxygen = maxOxygen;
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void Update()
    {
        float depth = Mathf.Abs(transform.position.y);
        float drainRate = (baseDrainRate + depth * depthMultiplier) * fastDrainMultiplier;
        currentOxygen -= drainRate * Time.deltaTime;

        if (oxygenBar != null)
            oxygenBar.value = currentOxygen / maxOxygen;

        if (currentOxygen <= 0)
        {
            currentOxygen = 0;
            levelManager.OnPlayerDrowned();
        }
    }

    public void RefillOxygen()
    {
        currentOxygen = maxOxygen;
        fastDrainMultiplier = 1f;
    }

    public void BoostOxygenDrain(float multiplier)
    {
        fastDrainMultiplier = multiplier;
    }
}
