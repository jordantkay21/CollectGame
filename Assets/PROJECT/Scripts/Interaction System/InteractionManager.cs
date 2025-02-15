using KayosStudios.TBD.InputLogic;
using System;
using UnityEngine;

namespace KayosStudios.TBD.InteractionSystem
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] bool canInteract;
        [SerializeField] float interactRange = 3f;
        [SerializeField] LayerMask interactableLayer;

        [SerializeField] Interactable currentInteractable;

        public static event Action<Interactable> OnInteractionEnter;
        public static event Action OnInteractionExit;

        private void OnEnable()
        {
            InputManager.OnInteractPressed += TryInteract;
            InputManager.CanInteract += () => { return canInteract; };
        }

        private void OnDisable()
        {
            InputManager.OnInteractPressed -= TryInteract;
            InputManager.CanInteract -= () => { return canInteract; };
        }

        private void LateUpdate()
        {
            FireRaycast();
        }

        private void FireRaycast()
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                DebugLogger.Log("InteractionManager", "Main camera not found to perform raycasting.");
                return;
            }

            //Get the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            //Convert thje screen point to a ray
            Ray ray = cam.ScreenPointToRay(screenCenter);

            bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer);

            //Debug Ray (Visible in Scene View)
            Debug.DrawRay(ray.origin, ray.direction * (hitSomething ? hit.distance : interactRange), hitSomething ? Color.green : Color.red);

            if (hitSomething)
            {
                DebugLogger.Log("InteractionManager", $"Raycast hit {hit.collider.gameObject.name}", DebugLevel.Verbose);
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    if (currentInteractable == interactable) return;

                    canInteract = true;
                    currentInteractable = interactable;
                    DebugLogger.Log("InteractionManager", "Interactable Object is in player's sight!");
                    OnInteractionEnter?.Invoke(interactable);
                }
            }
            else
            {
                canInteract = false;
                currentInteractable = null;
                DebugLogger.Log("InteractionManager", "No interactable objects found in sight!");
                OnInteractionExit?.Invoke();
            }
        }

        private void TryInteract()
        {
            DebugLogger.Log("InteractionManager", "Attempting to Interact", DebugLevel.Verbose);
            currentInteractable?.Interact();
        }
    }
}