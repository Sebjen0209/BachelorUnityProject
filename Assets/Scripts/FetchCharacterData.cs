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
                    Players[] players = JsonConvert.DeserializeObject<Players[]>(webRequest.downloadHandler.text);
                    Debug.Log($"Player: {players[1].playerName} {players[1].hp}");
                    break;
            }
        }
    }
}