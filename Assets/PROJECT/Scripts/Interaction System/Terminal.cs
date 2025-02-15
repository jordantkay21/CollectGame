using UnityEngine;
using UnityEngine.Events;

namespace KayosStudios.TBD.InteractionSystem.Object
{
    public class Terminal : Interactable
    {
        public UnityEvent OnTerminalAccess;

        public override void Interact()
        {
            base.Interact();

            DebugLogger.Log("Terminal", "Terminal Accessed!", DebugLevel.Verbose);
            OnTerminalAccess?.Invoke();
        }

        protected override string GetDisplayMessage()
        {
            return CheckTransaction();
        }
    }
}