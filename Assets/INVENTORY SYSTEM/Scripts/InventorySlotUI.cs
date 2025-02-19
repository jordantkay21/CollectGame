using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KayosStudios.InventorySystem.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] Image itemIcon;
        [SerializeField] TMP_Text itemNameText;
        [SerializeField] TMP_Text itemCountText;

        public void ConfigureSlot(Sprite icon, string name, int count)
        {
            itemIcon.sprite = icon;
            itemNameText.text = name;
            itemCountText.text = count.ToString();
        }
    }
}