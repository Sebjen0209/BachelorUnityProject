using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;

    // Public property to access the singleton instance
    public string accountApiData = "";
    public string characterAccountAPIData = "";
    public int hp;
    public static GameManager Instance
    {
        get
        {
            // If there is no instance yet, find it or create a new one
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                // If no instance exists in the scene, create a new GameObject and attach this script
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(GameManager).Name);
                    _instance = singletonObject.AddComponent<GameManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    
    public List<Characters> CharactersData { get; set; } = new List<Characters>();
    
    public Characters SelectedCharacter { get; set; }
    
    [System.Serializable]
    public class Characters
    {
        public int Id;
        public string Name;
        public string ClassType;
        public int Level;
    }

    // Ensure the instance is not destroyed when loading a new scene
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
