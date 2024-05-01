using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FetchCharacterData : MonoBehaviour
{
    void Start() {
        StartCoroutine(fetchPlayerStats("http://localhost:3001/playerStats"));
    }
    
    IEnumerator fetchPlayerStats(string uri){
        using(UnityWebRequest webRequest = UnityWebRequest.Get(uri)){
            yield return webRequest.SendWebRequest();

            switch(webRequest.result){
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("ConnectionError");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("DataProcessingError");    
                    break;
                case UnityWebRequest.Result.Success:
                    try {
                        GameManager.Characters[] players = JsonConvert.DeserializeObject<GameManager.Characters[]>(webRequest.downloadHandler.text);
                        if (players != null && players.Length > 1) {
                            Debug.Log($"Player: {players[1].Name}, ATK: {players[1].ClassType}");
                        } else {
                            Debug.Log("No players data or insufficient players data.");
                        }
                    } catch (System.Exception ex) {
                        Debug.LogError($"Error parsing JSON: {ex.Message}");
                    }
                    break;
            }
        }
    }
    
}