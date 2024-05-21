using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class CharacterSceneController : MonoBehaviour
{
    void Start()
    {
        // Call the method to fetch API data
        StartCoroutine(FetchData());
    }

    IEnumerator FetchData()
    {
        ButtonSetup bs = new ButtonSetup();

        // Construct the URL with the user ID

        string apiAccountData = GameManager.Instance.accountApiData;
        string formattedApiAccountData = apiAccountData.Split(":")[1].Replace("\"", "").Split("}")[0];

        string url = "https://localhost:7124/api/data/" + formattedApiAccountData;

        Debug.Log("url call from charactersecene " + url);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            try
            {
                string[] jsonStrings = JsonConvert.DeserializeObject<string[]>(www.downloadHandler.text);
                List<GameManager.Characters> characters = new List<GameManager.Characters>();

                foreach (string jsonString in jsonStrings)
                {
                    GameManager.Characters character =
                        JsonConvert.DeserializeObject<GameManager.Characters>(jsonString);
                    characters.Add(character);
                }

                GameManager.Instance.CharactersData = characters;

                // Example of logging the first character's name
                if (characters.Count > 0)
                {
                    Debug.Log(
                        $"Name: {characters[0].Name}, Level: {characters[0].Level}, ClassType: {characters[0].ClassType}");
                }
                else
                {
                    Debug.Log("No characters data.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
        }
    }
}
