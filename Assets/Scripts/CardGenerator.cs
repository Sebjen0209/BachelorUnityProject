using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CardGenerator : MonoBehaviour
{
    public GameObject cardPrefab; // Assign your card prefab in the Inspector
    public Transform cardParent; // Assign 'Content' GameObject here in the Inspector

    void Start()
    {
        // Optional: delay card generation to wait for data loading
        StartCoroutine(WaitAndGenerateCards());
    }

    IEnumerator WaitAndGenerateCards()
    {
        while (GameManager.Instance.CharactersData.Count == 0)
        {
            yield return new WaitForSeconds(0.5f); // Check every half second
        }
        GenerateCards();
    }

    private void GenerateCards()
    {
        Debug.Log("Attempting to generate cards. Current data count: " + GameManager.Instance.CharactersData.Count);

        foreach (GameManager.Characters character in GameManager.Instance.CharactersData)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            
            //newCard.name = character.Name; // Helpful for debugging
            TextMeshProUGUI textComponent = newCard.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = character.Name + "\nClass: " + character.ClassType + "\nLevel: " + character.Level;
            }
            else
            {
                Debug.LogError("TextMeshPro component not found on the card prefab!");
            }
        }
    }
}
