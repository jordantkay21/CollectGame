using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] KeyCode interactKey = KeyCode.E;

    public static event Action OnInteractPressed;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            DebugLogger.Log("PlayerInput", $"Player pressed Interaction Key {interactKey}");
            OnInteractPressed?.Invoke();
        }
    }
}
