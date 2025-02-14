using UnityEngine;

namespace KayosStudios.TBD.InteractionSystem.Object
{
    public class Button : Interactable
    {
        public override void Interact()
        {
            DebugLogger.Log("Button", "Button Pressed!", DebugLevel.Verbose);
        }

    }
}