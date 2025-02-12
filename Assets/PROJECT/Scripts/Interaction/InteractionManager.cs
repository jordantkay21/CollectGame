using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class InteractionManager : MonoBehaviour
{
    [SerializeField] Transform rayOrigin;
    [SerializeField] float interactRange = 3f;
    [SerializeField] LayerMask interactableLayer;

    private IInteractable currentInteractable;

    public static event Action<bool> OnInteractionAvailable;

    private void OnEnable()
    {
        PlayerInput.OnInteractPressed += TryInteract;
    }

    private void OnDisable()
    {
        PlayerInput.OnInteractPressed -= TryInteract;
    }

    private void FixedUpdate()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer);

        //Debug Ray (Visible in Scene View)
        Debug.DrawRay(ray.origin, ray.direction * (hitSomething ? hit.distance : interactRange), hitSomething ? Color.green : Color.red);

        if (hitSomething)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (currentInteractable == interactable) return;

                currentInteractable = interactable;
                OnInteractionAvailable?.Invoke(true);
                DebugLogger.Log("Interactions", "Interactable Object is in player's sight and is in range!", DebugLevel.Verbose);
                return;
            }

            if (currentInteractable != null)
            {
                currentInteractable = null;
                OnInteractionAvailable?.Invoke(false);
                DebugLogger.Log("Interactions", "No interactable objects found in range!", DebugLevel.Verbose);
            }
        }
       
    }

    private void TryInteract()
    {
        DebugLogger.Log("Interactions", "Attempting to Interact.", DebugLevel.Verbose);
        currentInteractable?.Interact();
    }
}
