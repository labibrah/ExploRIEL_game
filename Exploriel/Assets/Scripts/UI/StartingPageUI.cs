using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingPageUI : MonoBehaviour
{
    public void StartGame()
    {
        // Load the quiz scene
        Debug.Log("Starting game...");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void LoadSettings()
    {
        // Load the settings scene
        Debug.Log("Loading settings...");
        //SceneManager.LoadScene("Settings");
    }

    public void LoadHelp()
    {
        // Load the help scene
        Debug.Log("Loading help...");
        //SceneManager.LoadScene("Help");
    }
}
