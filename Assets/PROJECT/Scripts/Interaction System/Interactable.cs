using System;
using UnityEngine;

namespace KayosStudios.TBD.InteractionSystem
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] string actionToExecute;

        public abstract void Interact();
        
        private void OnEnable()
        {
            InteractionManager.GetActionToExecute += () => { return actionToExecute; };
        }

    }
}