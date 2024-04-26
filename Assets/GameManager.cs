using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;

    // Public property to access the singleton instance
    public string accountApiData = "";

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
                }
            }
            return _instance;
        }
    }

    // Add your global variables here
    public int score;

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
