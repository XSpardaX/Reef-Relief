using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private TrashItem[] allTrash;
    public Image[] starImages;
    public Sprite star;
    private float maxStar = 20f;
    private float SecondStar = 30f;
    public GameObject missionCompleteOverlay;

    private UIManager uIManager;
    private bool levelEnded = false;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {
        allTrash = FindObjectsByType<TrashItem>(FindObjectsSortMode.None);
        uIManager = FindObjectOfType<UIManager>();
    }

    public void CheckAllTrashCollected()
    {
        if (allTrash == null) return;

        foreach (var trash in allTrash)
        {
            if (trash == null) continue; // Destroyed object
            if (!trash.isCollected) return;
        }

        EndLevel();
    }

    public void OnTrashDeposited(int amount)
    {
        Debug.Log($"Player deposited {amount} trash items.");

        CheckAllTrashCollected();
    }

    public void OnPlayerDrowned()
    {
        Debug.Log("Player drowned — restarting level.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void EndLevel()
    {
        if (levelEnded) return;
        levelEnded = true;

        Debug.Log("All trash collected — mission complete!");

        AwardStarsBasedOnTime();

        if (missionCompleteOverlay != null)
        {
            missionCompleteOverlay.SetActive(true);
        }
        
        if (uIManager != null)
        {
            uIManager.StopTimer();
        }

        Time.timeScale = 0f; // pause gameplay input
    }

    private void AwardStarsBasedOnTime()
    {
        if (uIManager == null || starImages == null) return;

        starImages[1].sprite = null;
        starImages[0].sprite = null;

        float elapsedTime = uIManager.GetElapsedTime();
        Debug.Log($"Level completed in: {elapsedTime:F1} seconds");

        int starCount = 0;

        if (elapsedTime <= maxStar)
        {
            starImages[1].sprite = star;
            starImages[0].sprite = star;
        }
        else if (elapsedTime <= SecondStar)
        {
            starImages[0].sprite = star;
        }
    }

    }