using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class MissionCompleteUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public TMP_Text timeText;
    public Image[] stars;
    public Button restartButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Image glowBackground; // ? NEW

    [Header("Star Settings")]
    public float threeStarTime = 30f;
    public float twoStarTime = 60f;
    public float fadeDuration = 1f;

    [Header("Glow Pulse Settings")]
    public float pulseSpeed = 1f;
    public float minAlpha = 0.1f;
    public float maxAlpha = 0.4f;
    public Color glowColor = new Color(0.4f, 1f, 1f, 0.3f);

    private bool isShowing = false;
    private bool isPulsing = false;

    void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        if (glowBackground != null)
        {
            glowBackground.color = new Color(glowColor.r, glowColor.g, glowColor.b, 0);
        }

        restartButton.onClick.AddListener(RestartLevel);
        mainMenuButton.onClick.AddListener(ReturnToMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void Show(float completionTime)
    {
        if (isShowing) return;
        isShowing = true;

        int starsEarned = 1;
        if (completionTime <= threeStarTime)
            starsEarned = 3;
        else if (completionTime <= twoStarTime)
            starsEarned = 2;

        timeText.text = $"Time: {completionTime:F1}s";
        StartCoroutine(FadeIn(starsEarned));
    }

    private IEnumerator FadeIn(int starsEarned)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = (i < starsEarned)
                ? Color.yellow
                : Color.gray;
        }

        // Begin pulse
        if (glowBackground != null)
        {
            StartCoroutine(GlowPulse());
        }

        Time.timeScale = 0f;
    }

    private IEnumerator GlowPulse()
    {
        isPulsing = true;
        float alpha = minAlpha;
        bool increasing = true;

        while (isPulsing)
        {
            if (glowBackground == null) yield break;

            float delta = Time.unscaledDeltaTime * pulseSpeed;
            if (increasing)
            {
                alpha += delta;
                if (alpha >= maxAlpha)
                {
                    alpha = maxAlpha;
                    increasing = false;
                }
            }
            else
            {
                alpha -= delta;
                if (alpha <= minAlpha)
                {
                    alpha = minAlpha;
                    increasing = true;
                }
            }

            Color c = glowColor;
            c.a = alpha;
            glowBackground.color = c;

            yield return null;
        }
    }

    void RestartLevel()
    {
        isPulsing = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ReturnToMenu()
    {
        isPulsing = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}