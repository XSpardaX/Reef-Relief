using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private TrashItem[] allTrash;
    public GameObject missionCompleteOverlay;
    private bool levelEnded = false;

    void Start()
    {
        allTrash = FindObjectsByType<TrashItem>(FindObjectsSortMode.None);
    }

    public void CheckAllTrashCollected()
    {
        foreach (var trash in allTrash)
        {
            if (trash != null && !trash.isCollected)
                return;
        }

        EndLevel();
    }

    public void OnTrashDeposited(int amount)
    {
        Debug.Log($"Player deposited {amount} trash items.");

        // Check if any trash remains
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

        if (missionCompleteOverlay != null)
        {
            missionCompleteOverlay.SetActive(true);
        }

        Time.timeScale = 0f; // pause gameplay input
    }
}