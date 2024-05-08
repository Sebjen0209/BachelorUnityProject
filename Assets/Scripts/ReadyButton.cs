using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInitialization : MonoBehaviour
{
    public Button readyButton;

    void Start()
    {
        readyButton.onClick.AddListener(OnReadyClicked);
    }

    void OnReadyClicked()
    {
        // Check if a character has been selected
        if (GameManager.Instance.SelectedCharacter != null)
        {
            Debug.Log("Starting game with character: " + GameManager.Instance.SelectedCharacter.Name);

            SceneManager.LoadScene("GameScene"); 
        }
        else
        {
            Debug.LogError("No character selected! Please select a character.");
        }
    }
}