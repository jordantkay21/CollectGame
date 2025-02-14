using KayosStudios.TBD.Inventory;
using KayosStudios.TBD.Player.Inputs;
using System;
using UnityEngine;

namespace KayosStudios.TBD.Interactables.Transac
{
    public abstract class Transactionible : MonoBehaviour, IInteractable
    {
        [SerializeField] protected ItemType requiredItem; //The item needed for interaction
        [SerializeField] protected int requiredAmount; // The amount needed
        [SerializeField] protected string actionToAccomplish;

        public static Func<ItemType, int, bool> HasEnoughItems;
        public static event Action<string> OnDisplayMessage;

        private void OnEnable()
        {
            InteractionManager.OnInteractionEnter += CheckTransaction;
            
        }

        public void CheckTransaction()
        {
            if (HasEnoughItems != null)
            {
                bool haveItems = HasEnoughItems.Invoke(requiredItem, requiredAmount);

                if (haveItems)
                {
                    DebugLogger.Log("Transactional", $"Transaction successful! Player has {requiredAmount} {requiredItem}(s) to execute the interaction.");
                    OnDisplayMessage?.Invoke($"Press '{InputManager.interactKey}' to {actionToAccomplish}. \n Action consumes {requiredAmount} {requiredItem}(s).");
                }
                else
                {
                    DebugLogger.Log("Transactional", "Not enough resources to complete the interaction.");
                    OnDisplayMessage?.Invoke($"Insufficent resources. Action requires {requiredAmount} {requiredItem}(s).");
                }
            }
            else
            {
                DebugLogger.Log("Transactional", "No Subscribers to Func.");
            }

            
        }

        public abstract void Interact();
    }
}