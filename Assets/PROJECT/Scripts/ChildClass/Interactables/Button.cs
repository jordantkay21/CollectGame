
using KayosStudios.TBD.Interactables;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable
{
    public UnityEvent OnButtonPress;

    public void Interact()
    {
        DebugLogger.Log("Button", "Button Pressed!", DebugLevel.Verbose);
        OnButtonPress?.Invoke();
    }
}
