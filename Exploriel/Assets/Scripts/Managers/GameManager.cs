using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerData playerData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
            playerData = new PlayerData();
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate managers
        }
    }

    void Start()
    {
        // Initialize player data or load from saved data
        SceneManager.LoadScene("StartingPage");
    }
}
// Add methods to save and load player data here
