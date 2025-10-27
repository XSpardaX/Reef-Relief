using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Oxygen UI")]
    public Slider oxygenBar;
    public TMP_Text oxygenText;

    [Header("Timer UI")]
    public TMP_Text timerText;

    [Header("Stars UI")]
    public Image[] starImages;
    public Color activeStarColor = Color.yellow;
    public Color inactiveStarColor = Color.gray;

    private float startTime;
    private bool timerRunning = true;

    void Start()
    {
        startTime = Time.time;
        UpdateOxygen(100);
        UpdateStars(0);
    }

    void Update()
    {
        if (timerRunning)
        {
            float elapsed = Time.time - startTime;
            UpdateTimer(elapsed);
        }
    }

    public void UpdateOxygen(float value)
    {
        oxygenBar.value = value;
        oxygenText.text = $"Oxygen: {Mathf.RoundToInt(value)}%";
    }

    public void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    public void UpdateStars(int count)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].color = (i < count) ? activeStarColor : inactiveStarColor;
        }
    }

    public void StopTimer() => timerRunning = false;
}