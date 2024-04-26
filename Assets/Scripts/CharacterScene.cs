using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

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

        Debug.Log("url call from charactersecene "+url);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || 
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // Parse and process the data
            string jsonData = www.downloadHandler.text;
            ProcessData(jsonData);
        }
    }

    void ProcessData(string jsonData)
    {
        // Parse and process the JSON data here
        Debug.Log("Received characters: " + jsonData);
        
    }
}
