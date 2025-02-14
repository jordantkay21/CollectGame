using System;
using UnityEngine;

namespace KayosStudios.TBD.Player.Inputs
{
    public class InputManager : MonoBehaviour
    {
        public static KeyCode interactKey = KeyCode.E;

        public static event Action OnInteractPressed;

        private void Update()
        {
            if (Input.GetKeyDown(interactKey))
            {
                DebugLogger.Log("PlayerInput", $"Player Pressed Interaction Key {interactKey}");
                OnInteractPressed?.Invoke();
            }
        }
    }
}