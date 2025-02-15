using KayosStudios.TBD.InputLogic;
using KayosStudios.TBD.InventorySystem;
using System;
using UnityEngine;

namespace KayosStudios.TBD.InteractionSystem
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interactable Settings")]
        [SerializeField] protected string actionToExecute;
        [SerializeField] protected bool canAfford;

        protected string displayMessage;
        public static event Action<string> SendDisplayMessage;


        [Header("Transactional Interactable Settings")]
        [SerializeField] protected bool isTransactional;
        [SerializeField] protected ItemType requiredItem;
        [SerializeField] protected int requiredAmount;

        public static event Action<ItemType, int> OnTransactionInitiation;

        public static Func<ItemType, int, bool> CanAfford;
        

        [Header("Trade Interactable Settings")]
        [SerializeField] protected bool givesItemInReturn;
        [SerializeField] protected ItemType itemToGive;
        [SerializeField] protected int amountToGive;


        public virtual void Interact()
        {
            if (!isTransactional) canAfford = true;
            if (!canAfford) return;

            if(isTransactional && canAfford)
            {
                OnTransactionInitiation(requiredItem, -requiredAmount);
            }
        }
        protected abstract string GetDisplayMessage();
        
        private void OnEnable()
        {
            InteractionManager.OnInteractionEnter += HandleInteractionEnter;

        }

        private void HandleInteractionEnter(Interactable interactable)
        {
            if (interactable == this)
            {
                SendDisplayMessage?.Invoke(GetDisplayMessage());
            }
        }


        protected string CheckTransaction()
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