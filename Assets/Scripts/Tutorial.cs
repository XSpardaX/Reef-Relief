using UnityEngine;
using UnityEngine.UI; 

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject nextPage;
    public GameObject UI;

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void NextPage()
    {
        nextPage.SetActive(true);
        nextPage.GetComponent<GraphicRaycaster>().enabled = true;
        tutorialPanel.SetActive(false);
    }
}
