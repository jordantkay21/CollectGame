using System;
using UnityEngine;

namespace KayosStudios.TBD.InputLogic
{
    public class InputManager : MonoBehaviour
    {
        public static KeyCode interactKey = KeyCode.E;

        public static event Func<bool> CanInteract;
        public static event Action OnInteractPressed;

        private void Update()
        {
            if (CanInteract != null)
            {
                bool canInteract = CanInteract.Invoke();

                if (Input.GetKeyDown(interactKey))
                {
                    if (canInteract)
                    {
                        DebugLogger.Log("PlayerInput", $"Interaction Approved. Player pressed interaction key [{interactKey}] with an interactable in range.");
                        OnInteractPressed?.Invoke();
                    }
                    else
                    {
                        DebugLogger.Log("PlayerInput", $"Interaction Denied. Player Pressed interaction key [{interactKey}] with no interactable in range.");
                    }
                }
            }
            else
            {
                DebugLogger.Log("PlayerInput", $"No subscribers detected for CanInteract. Interactable Manager should be involved.", DebugLevel.Error);
            }

        }
    }
}