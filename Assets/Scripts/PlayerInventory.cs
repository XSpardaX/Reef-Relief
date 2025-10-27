using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int trashCount = 0;
    public int maxTrash = 5;
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
       // if (trashText)
       //     trashText.text = $"Trash: {trashCount}/{maxTrash}";
    }
}