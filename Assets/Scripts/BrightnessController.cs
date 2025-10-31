using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class BrightnessController : MonoBehaviour
{
    [Header("UI")]
    public Slider brightnessSlider;          // Drag the slider here
    public Image brightnessOverlay;          // A full-screen dark overlay

    private const string BrightnessKey = "BrightnessValue";

    private void Awake()
    {
        // Create overlay if you didn't make one manually
        if (brightnessOverlay == null)
            brightnessOverlay = CreateOverlay();

        // Load saved brightness (default 0.5)
        float saved = PlayerPrefs.GetFloat(BrightnessKey, 0.5f);
        SetBrightness(saved);
        if (brightnessSlider != null)
            brightnessSlider.value = saved;
    }

    // Called from the slider's OnValueChanged
    public void OnBrightnessChanged(float value)
    {
        SetBrightness(value);
        PlayerPrefs.SetFloat(BrightnessKey, value);
        PlayerPrefs.Save();
    }

    private void SetBrightness(float sliderValue)
    {
        // 0 = full dark, 1 = full bright
        // We map it to an alpha of 0 (bright) ? 0.8 (dark)
        float alpha = 1f - sliderValue;               // 1 ? 0, 0 ? 1
        alpha = Mathf.Clamp01(alpha) * 0.8f;          // max 80% dark

        Color c = brightnessOverlay.color;
        c.a = alpha;
        brightnessOverlay.color = c;

        if (brightnessSlider != null)
            brightnessSlider.value = sliderValue;
    }

    private Image CreateOverlay()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in scene! Brightness overlay cannot be created.");
            return null;
        }

        GameObject go = new GameObject("BrightnessOverlay", typeof(Image), typeof(CanvasGroup));
        go.transform.SetParent(canvas.transform, false); // ? PARENT TO CANVAS!

        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        cg.blocksRaycasts = false;
        cg.interactable = false;

        Image img = go.GetComponent<Image>();
        img.color = new Color(0, 0, 0, 0);
        img.raycastTarget = false;

        RectTransform rt = img.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;

        // Put behind all UI
        go.transform.SetAsFirstSibling();

        return img;
    }
}
