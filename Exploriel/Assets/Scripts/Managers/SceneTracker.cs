using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static SceneTracker Instance;

    public string previousSceneName;
    public Vector3 playerReturnPosition;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void RecordSceneAndPosition(Vector3 playerPos)
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        playerReturnPosition = playerPos;
    }

    public void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("No previous scene recorded!");
        }
    }
}
