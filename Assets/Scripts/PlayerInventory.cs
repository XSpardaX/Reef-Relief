using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int trashCount = 0;
    public int maxTrash = 5;

    public TMP_Text trashHeld;
    //public TextMeshProUGUI trashText;

    public bool CanCollect() => trashCount < maxTrash;

    public void CollectTrash()
    {
        trashCount++;
        UpdateUI();
    }

    public int DepositTrash()
    {
        int deposited = trashCount;
        trashCount = 0;
        UpdateUI();
        return deposited;
    }

    private void UpdateUI()
    {
        if (trashCount == maxTrash)
        {
            trashHeld.text = "Deposit garbage: " + trashCount + "/" + maxTrash;
        }
        else { trashHeld.text = "Gaarbage held: " + trashCount + "/" + maxTrash; }
    }
}