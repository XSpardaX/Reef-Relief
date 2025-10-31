using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject settingsPanel;
    public GameObject UI;
    public GameObject Complete;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    private const string FullscreenKey = "IsFullscreen";

    private void Start()
    {

    }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Player drowned — restarting level.");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main menu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level");
        Time.timeScale = 0f;
        UI.SetActive(false);
        Complete.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
    }

    public void PauseGame()
    {
        settingsPanel.SetActive(true);
        UI.SetActive(false);

        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        settingsPanel.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnVolumeChanged()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
}

