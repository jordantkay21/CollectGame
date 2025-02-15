using KayosStudios.TBD.InputLogic;
using UnityEngine;
using UnityEngine.Events;

namespace KayosStudios.TBD.InteractionSystem.Object
{
    public class Door : Interactable
    {
        [Header("Door Settings")]
        [SerializeField] bool isLocked;
        [SerializeField] bool isOpen;
        [SerializeField] Animator anim;
        [SerializeField] GameObject lockIcon;

        public override void Interact()
        {
            base.Interact();

            if (isLocked) return;

            if (isOpen)
            {
                DebugLogger.Log("Door", "Closing Door");
                anim.SetTrigger("Close");
                isOpen = false;
                actionToExecute = "Open Door";
            }
            else
            {
                DebugLogger.Log("Door", "Opening Door");
                anim.SetTrigger("Open");
                isOpen = true;
                actionToExecute = "Close Door";
            }
        }

        public void Unlock()
        {
            lockIcon.SetActive(false);
            isLocked = false;
        }

        protected override string GetDisplayMessage()
        {
            if (isLocked) return "Unlock Door from Terminal";

            displayMessage = $"Press {InputManager.interactKey} to {actionToExecute}";
            return displayMessage;
        }
    }
}
