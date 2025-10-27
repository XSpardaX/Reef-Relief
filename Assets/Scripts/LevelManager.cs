using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<Trashitem> trashItems; // Assign all trash in inspector
    public OxygenSystem oxygenSystem;
    public Text timerText;
    public Image[] starImages; // 3 stars in UI
    public Sprite filledStar;
    public Sprite emptyStar;

    public float timeLimit = 60f; // full 3-star time
    public float timer;
    private bool levelComplete = false;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        if (levelComplete) return;

        timer += Time.deltaTime;
        UpdateTimerUI();

        CheckAllTrashCollected();
    }

    public void CheckAllTrashCollected()
    {
        // Make sure the list exists
        if (trashItems == null || trashItems.Count == 0)
            return;

        bool allCollected = true;

        foreach (var item in trashItems)
        {
            if (!item.isCollected)
            {
                allCollected = false;
                break;
            }
        }

        if (allCollected)
        {
            levelComplete = true;
            EvaluateStarRating();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = "Time: " + timer.ToString("F1");
    }

    void EvaluateStarRating()
    {
        float timeTaken = timer;

        int starsEarned = 1;
        if (timeTaken <= timeLimit) starsEarned = 3;
        else if (timeTaken <= timeLimit * 1.5f) starsEarned = 2;

        DisplayStars(starsEarned);
    }

    void DisplayStars(int count)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = i < count ? filledStar : emptyStar;
        }
    }

    public void OnTrashDeposited(int deposited)
    {
        Debug.Log($"Player deposited {deposited} trash items!");

        // Optional: Add score tracking logic here if needed
        // e.g. totalScore += deposited;
    }

    public void OnPlayerDrowned()
    {
        Debug.Log("Player drowned! Level failed.");
        levelComplete = true;

        // Optional: Stop timer or trigger UI
        if (timerText != null)
            timerText.text += " - Drowned!";

        // Optionally show failure UI or restart level:
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
