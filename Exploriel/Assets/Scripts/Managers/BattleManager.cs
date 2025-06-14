using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public GameObject syntaxShufflePanel;
    public SyntaxShuffleManager syntaxShuffleManager;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public Canvas winScreen;
    public Canvas loseScreen;

    public float turnDelay = 1.5f;
    private bool isPlayerTurn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        isPlayerTurn = true;
        syntaxShufflePanel.SetActive(true);
        syntaxShuffleManager.StartNewChallenge(this); // Give reference to callback to return result
    }

    public void OnPlayerSubmitted(bool success)
    {
        syntaxShufflePanel.SetActive(false);
        Debug.Log("Player submitted answer: " + (success ? "Correct" : "Incorrect"));

        if (success)
        {
            enemyPrefab.GetComponent<Enemy>().takeDamage(1); // Or calculate based on difficulty
            audioSource.PlayOneShot(correctSound, 1.5f); // Play at 150% volume
        }
        else
        {
            playerPrefab.GetComponent<PlayerMovement>().takeDamage(1);
            audioSource.PlayOneShot(incorrectSound);
        }

        if (CheckBattleOver())
        {
            StartCoroutine(EndBattle());
        }
        else
        {
            Invoke(nameof(StartPlayerTurn), turnDelay);
        }


    }

    bool CheckBattleOver()
    {
        // Check if the enemy GameObject is active in the scene
        if (enemyPrefab != null && enemyPrefab.activeInHierarchy == false)
        {
            Debug.Log("Player Wins!");
            audioSource.PlayOneShot(winSound, 1.5f);
            winScreen.gameObject.SetActive(true);
            return true;
        }

        if (playerPrefab != null && playerPrefab.activeInHierarchy == false)
        {
            Debug.Log("Player Loses!");
            audioSource.PlayOneShot(loseSound);
            loseScreen.gameObject.SetActive(true);
            return true;
        }

        return false;
    }

    System.Collections.IEnumerator EndBattle()
    {
        // Stop the original background music
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Play the win sound
        audioSource.PlayOneShot(winSound);

        // Wait until player presses the "E" key
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }

        // Load the world1 scene
        SceneManager.LoadScene("world1");
    }
}
