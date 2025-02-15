using KayosStudios.TBD.InteractionSystem;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Display Message Configuration")]
    [SerializeField] GameObject displayMessagePanel;
    [SerializeField] TextMeshProUGUI displayMessageText;

    private void OnEnable()
    {
        InteractionManager.OnInteractionEnter += (interactable) => EnableDisplayMessage(true);
        InteractionManager.OnInteractionExit += () => EnableDisplayMessage(false);

        Interactable.SendDisplayMessage += SetDisplayMessage;
    }

    private void EnableDisplayMessage(bool isActive)
    {
        displayMessagePanel.SetActive(isActive);
    }

    private void SetDisplayMessage(string message)
    {
        displayMessageText.text = message;
    }
}
