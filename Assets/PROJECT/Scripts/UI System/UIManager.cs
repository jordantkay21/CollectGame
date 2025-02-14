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
        InteractionManager.OnInteractionEnter += (string message) =>
        {
            displayMessagePanel.SetActive(true);
            displayMessageText.text = message;
        };
        InteractionManager.OnInteractionExit += () => { displayMessagePanel.SetActive(false); };
    }
}
