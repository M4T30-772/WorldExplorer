using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplay : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private Text staminaText; // Reference to the Text component

    private void Start()
    {
        staminaText = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateStaminaText();
    }

    private void UpdateStaminaText()
    {
        if (playerMovement != null)
        {
            float currentStamina = playerMovement.currentStamina;
            staminaText.text = "Stamina: " + currentStamina.ToString("0");
        }
    }
}
