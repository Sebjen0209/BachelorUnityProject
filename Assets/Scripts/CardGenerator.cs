using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    public void SelectCharacter(GameManager.Characters character)
    {
        GameManager.Instance.SelectedCharacter = character;
        Debug.Log("Character selected: " + character.Name);
    }
    
    private void GenerateCards()
    {
        Debug.Log("Attempting to generate cards. Current data count: " + GameManager.Instance.CharactersData.Count);

        foreach (GameManager.Characters character in GameManager.Instance.CharactersData)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);

            
            Transform characterInfo = newCard.transform.Find("CaaracterInfo");
            if (characterInfo != null)
            {
                characterInfo.Find("Name").GetComponent<TextMeshProUGUI>().text = character.Name;
                characterInfo.Find("Information").GetComponent<TextMeshProUGUI>().text = @$"Level {character.Level} {character.ClassType.ToString().ToUpper()}";
            }
            else
            {
                Debug.LogError("Child named 'CharacterInfo' not found on the card prefab!");
            }

            Transform characterSelectInfoTop = newCard.transform.Find("CharacterSelectInfoTop");
            if (characterSelectInfoTop != null)
            {
                Transform roleIcon = characterSelectInfoTop.Find("RoleIcon");
                if (roleIcon != null)
                {
                    string roleIconPath = $"RoleIcons/set_icon_role_{character.ClassType}";
                    Sprite roleIconSprite = Resources.Load<Sprite>(roleIconPath);
                    if (roleIconSprite != null)
                    {
                        roleIcon.GetComponent<Image>().sprite = roleIconSprite;
                    }
                    else
                    {
                        Debug.LogError($"Role icon sprite for role {character.ClassType} not found!");
                    }
                }
                else
                {
                    Debug.LogError("Child named 'RoleIcon' not found on the card prefab!");
                }
            }
            else
            {
                Debug.LogError("Child named 'CharacterSelectInfoTop' not found on the card prefab!");
            }

            Transform characterC = newCard.transform.Find("Character");
            if (characterC != null)
            {
                Transform image = characterC.Find("image");
                if (image != null){
                    string iamgePath = $"RoleImages/Character_sample_{character.ClassType}";

                    Sprite imageSprite = Resources.Load<Sprite>(iamgePath);
                    if (imageSprite != null)
                    {
                        image.GetComponent<Image>().sprite = imageSprite;
                    }
                    else
                    {
                        Debug.LogError($"Image sprite for role {character.ClassType} not found!");
                    }
                }
            }


            Button cardButton = newCard.GetComponent<Button>();
            if (cardButton != null)
            {
                cardButton.onClick.AddListener(() => SelectCharacter(character));
            }
            else
            {
                Debug.LogError("Button component not found on card prefab!");
            }
            
        }
    }
    
}
