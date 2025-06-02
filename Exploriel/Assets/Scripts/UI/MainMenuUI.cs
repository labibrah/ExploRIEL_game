using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void LoadWorld1()
    {
        // Load the world 1 scene
        Debug.Log("Loading World 1...");
        //SceneManager.LoadScene("World1");
    }

    public void LoadWorld2()
    {
        // Load the world 2 scene
        Debug.Log("Loading World 2...");
        //SceneManager.LoadScene("World2");
    }

    public void LoadWorld3()
    {
        // Load the world 3 scene
        Debug.Log("Loading World 3...");
        //SceneManager.LoadScene("World3");
    }

    public void LoadWorld4()
    {
        // Load the world 4 scene
        Debug.Log("Loading World 4...");
        //SceneManager.LoadScene("World4");
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

    public void LoadProfile()
    {
        // Load the profile scene
        Debug.Log("Loading profile...");
        //SceneManager.LoadScene("Profile");
    }
}
