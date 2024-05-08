using UnityEngine;
using TMPro; // Add this to use TextMeshPro components

public class PlayerSetup : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.SelectedCharacter != null)
        {
            // Assuming your player prefab has components that need to be setup
            SetupPlayer(GameManager.Instance.SelectedCharacter);
        }
        else
        {
            Debug.LogError("No selected character data found on game start.");
        }
    }

    void SetupPlayer(GameManager.Characters character)
    {
        // Log to verify we're getting character data
        Debug.Log("Received character: " + character.Name);

        // Set player name using TextMeshPro
        var playerName = GetComponentInChildren<TextMeshPro>();
        if (playerName != null)
        {
            playerName.text = character.Name;
            Debug.Log("Player name set to: " + character.Name);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found.");
        }

        // Example: Assigning class or other properties
        Debug.Log("Setting up player as a " + character.ClassType + " at level " + character.Level);
    }
}