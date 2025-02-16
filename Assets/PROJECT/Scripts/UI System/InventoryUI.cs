using UnityEngine;
using TMPro;
using KayosStudios.TBD.InventorySystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI keyCardAmount;

    private void OnEnable()
    {
        InventoryManager.OnInventoryChange += UpdateUI;
    }

    private void UpdateUI(ItemType item, int amount)
    {
        switch (item)
        {
            case ItemType.Keycard:
                keyCardAmount.text = amount.ToString();
                break;
            default:
                break;
        }
    }
}
