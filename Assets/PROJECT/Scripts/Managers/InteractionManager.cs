using KayosStudios.TBD.Player.Inputs;
using System;
using UnityEngine;

namespace KayosManager.TBD.Interactions
{
    public interface IInteractable
    {
        void Interact();
    }

    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] float interactRange = 3f;
        [SerializeField] LayerMask interactableLayer;

        private IInteractable currentInteractable;

        public static event Action<bool> OnInteractionAvailable;

        private void OnEnable()
        {
            InputManager.OnInteractPressed += TryInteract;
        }

        private void OnDisable()
        {
            InputManager.OnInteractPressed -= TryInteract;
        }

        private void LateUpdate()
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                DebugLogger.Log("Interactions", "Main Camera not found to perform raycasting", DebugLevel.Error);
                return;
            }

            //Get the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            //Convert the screen point to a ray
            Ray ray = cam.ScreenPointToRay(screenCenter);

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

                if(currentInteractable != null)
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
}