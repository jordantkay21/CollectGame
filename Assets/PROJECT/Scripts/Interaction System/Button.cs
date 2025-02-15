using UnityEngine;
using UnityEngine.Events;

namespace KayosStudios.TBD.InteractionSystem.Object
{
    public class Button : Interactable
    {
        public UnityEvent OnButtonPress;

        public override void Interact()
        {
            base.Interact();

            DebugLogger.Log("Button", "Button Pressed!", DebugLevel.Verbose);
            OnButtonPress?.Invoke();
        }

    }
}