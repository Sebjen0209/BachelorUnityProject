using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class CreateCharacterSelectButton : MonoBehaviour
{
    public TMP_InputField characterNameInput;

    void Start()
    {
        // Optional: Add a listener to the button if necessary
    }

    void Update()
    {
        
    }

    public void CreateCharacter()
    {
        string accountId = GameManager.Instance.accountApiData.Split(":")[1].Replace("\"", "").Split("}")[0];
        string characterName = characterNameInput.text;
        string characterType = CharacterSelector.Instance.SelectedCharacterType;

        StartCoroutine(PostCharacterData(accountId, characterName, characterType));
    }

    IEnumerator PostCharacterData(string accountId, string characterName, string characterType)
    {
        string jsonBody = $"{{\"id\":\"{accountId}\",\"name\":\"{characterName}\",\"classtype\":\"{characterType}\"}}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest www = new UnityWebRequest("http://13.60.46.33", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Data received and processed successfully!");
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }
}