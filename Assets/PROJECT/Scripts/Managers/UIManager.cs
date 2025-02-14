using KayosStudios.TBD.Interactables;
using KayosStudios.TBD.Interactables.Transac;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Display Message Configuration")]
    [SerializeField] GameObject displayMessagePanel;
    [SerializeField] TextMeshProUGUI displayMessageText;

    private void OnEnable()
    {
        InteractionManager.OnInteractionEnter += () => displayMessagePanel.SetActive(true);
        Transactionible.OnDisplayMessage += (string message) => displayMessageText.text = message;
        InteractionManager.OnInteractionExit += () => displayMessagePanel.SetActive(false);

    }
}
