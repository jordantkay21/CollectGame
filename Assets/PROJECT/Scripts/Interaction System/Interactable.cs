using KayosStudios.TBD.InputLogic;
using KayosStudios.TBD.InventorySystem;
using System;
using UnityEngine;

namespace KayosStudios.TBD.InteractionSystem
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interactable Settings")]
        [SerializeField] string actionToExecute;
        [SerializeField] bool canAfford;

        private string displayMessage;
        public static event Action<string> SendDisplayMessage;


        [Header("Transactional Interactable Settings")]
        [SerializeField] bool isTransactional;
        [SerializeField] ItemType requiredItem;
        [SerializeField] int requiredAmount;

        public static Func<ItemType, int, bool> CanAfford;
        

        [Header("Trade Interactable Settings")]
        [SerializeField] bool givesItemInReturn;
        [SerializeField] ItemType itemToGive;
        [SerializeField] int amountToGive;


        public virtual void Interact()
        {
            if (!canAfford) return;
        }
        
        private void OnEnable()
        {
            InteractionManager.OnInteractionEnter += () => SendDisplayMessage?.Invoke(GetDisplayMessage());

        }

        private string GetDisplayMessage()
        {
            if (!isTransactional)
            {
                canAfford = true;
                displayMessage = $"Press {InputManager.interactKey} to {actionToExecute}";
                return displayMessage;
            }
            else
            {
                return CheckTransaction();
            }
        }

        private string CheckTransaction()
        {
            if(CanAfford != null)
            {
                canAfford = CanAfford.Invoke(requiredItem, requiredAmount);

                if (canAfford)
                {
                    DebugLogger.Log("Interactable", $"Transaction successful! Player has {requiredAmount} {requiredItem}(s) to execute the interaction.");
                    return $"Press {InputManager.interactKey} to {actionToExecute}. \n Action consumes {requiredAmount} {requiredItem}(s)";
                }
                else
                {
                    DebugLogger.Log("Interactable", $"Insufficent resources! Action requires {requiredAmount} {requiredItem}(s).");
                    return $"Insufficent resources! Spend {requiredAmount} {requiredItem}(s) to {actionToExecute}";
                }
            }
            else
            {
                DebugLogger.Log("Interactable", $"No subscribers to Func[CanAfford], check Inventory Manager");
                return null;
            }
        }

    }
}