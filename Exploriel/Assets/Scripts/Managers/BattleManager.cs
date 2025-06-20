using System.Collections;
using TMPro;
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
    public TextMeshProUGUI UItext;
    public string[] battleMessages = {
        "Get ready for a syntax challenge!",
        "Can you solve this?",
        "Time to test your skills!",
        "Let's see what you've got!",
        "Prepare for a syntax showdown!"
    };
    public float messageDisplayTime = 2f; // Time to display each message
    public float messageDelay = 0.5f; // Delay between messages
    public string[] PerfectAnswerMessages = {
        "Great job!",
        "Well done!",
        "Nice work!",
        "You nailed it!",
        "Excellent!"
    };
    public string[] GoodAnswerMessages = {
        "Good effort!",
        "Not bad!",
        "Keep it up!",
        "You're getting there!",
        "Solid attempt!"
    };

    public string[] NeutralAnswerMessages = {
        "That's okay!",
        "That's fine!",
        "Keep trying!",
        "You're on the right track!",
        "Don't worry, you'll improve!"
    };
    public string[] WrongAnswerMessages = {
        "Try again!",
        "Don't give up!",
        "You can do better!",
        "Keep practicing!",
        "Almost there!"
    };

    public float turnDelay = 1.5f;
    private bool isPlayerTurn = true;
    public float battleDuration = 30f; // Total duration of the battle

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(RunBattleIntroAndStartTurn());

        // Add this coroutine to handle the sequence
        IEnumerator RunBattleIntroAndStartTurn()
        {
            yield return StartCoroutine(ShowBattleMessages());
            yield return StartCoroutine(StartTurn());
        }
    }

    IEnumerator StartTurn()
    {
        Debug.Log("Starting turn. Is Player's turn: " + isPlayerTurn);
        yield return StartCoroutine(showTurnMessage());
        syntaxShufflePanel.SetActive(true);
        syntaxShuffleManager.StartNewChallenge(this); // Give reference to callback to return result
    }

    public void OnPlayerSubmitted(bool success, float timeRemaining)
    {
        syntaxShufflePanel.SetActive(false);
        Debug.Log("Player submitted answer: " + (success ? "Correct" : "Incorrect"));

        if (success)
        {
            float damage = 0f;
            string message = "";
            float soundVolume = 1f;

            if (timeRemaining > 0.8f * battleDuration)
            {
                damage = 1f;
                message = PerfectAnswerMessages[Random.Range(0, PerfectAnswerMessages.Length)];
                soundVolume = 2f;
            }
            else if (timeRemaining > 0.5f * battleDuration)
            {
                damage = 1f;
                message = GoodAnswerMessages[Random.Range(0, GoodAnswerMessages.Length)];
                soundVolume = 1.5f;
            }
            else
            {
                damage = 0.5f;
                message = NeutralAnswerMessages[Random.Range(0, NeutralAnswerMessages.Length)];
                soundVolume = 1f;
            }

            audioSource.PlayOneShot(correctSound, soundVolume);

            if (isPlayerTurn)
            {
                playerPrefab.GetComponent<PlayerMovement>().AttackEnemy();
                Debug.Log("Player dealt " + damage + " damage to the enemy.");
                enemyPrefab.GetComponent<FightingEnemy>().takeDamage(damage);
                isPlayerTurn = (timeRemaining > 0.8f * battleDuration); // Only keep turn if perfect
            }
            else
            {
                if (damage < 1f)
                    playerPrefab.GetComponent<PlayerMovement>().takeDamage(damage);
                else
                    enemyPrefab.GetComponent<FightingEnemy>().takeDamage(damage);

                isPlayerTurn = true; // Player regains turn after enemy
            }
            StartCoroutine(ShowTextWithDelay(message, messageDisplayTime));
        }
        else
        {
            string message = WrongAnswerMessages[Random.Range(0, WrongAnswerMessages.Length)];
            float soundVolume = isPlayerTurn ? 1.5f : 1f;
            audioSource.PlayOneShot(incorrectSound, soundVolume);

            if (!isPlayerTurn)
            {
                playerPrefab.GetComponent<PlayerMovement>().takeDamage(1);
            }
            else
            {
                playerPrefab.GetComponent<PlayerMovement>().AttackEnemy();
                enemyPrefab.GetComponent<FightingEnemy>().Doge();
            }

            isPlayerTurn = !isPlayerTurn;
            StartCoroutine(ShowTextWithDelay(message, messageDisplayTime));
        }

        if (CheckBattleOver())
        {
            StartCoroutine(EndBattle());
        }
        else
        {
            Debug.Log("Battle continues. Next turn: " + (isPlayerTurn ? "Player" : "Enemy"));
            StartCoroutine(StartTurn());
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

    public IEnumerator ShowTextWithDelay(string message, float delay)
    {

        UItext.text = message;

        yield return new WaitForSeconds(delay);

        UItext.text = string.Empty; // Clear the text after the delay
    }

    public IEnumerator ShowBattleMessages()
    {
        foreach (string message in battleMessages)
        {
            UItext.text = message;
            audioSource.PlayOneShot(correctSound, 0.5f);
            yield return new WaitForSeconds(messageDisplayTime + messageDelay);
        }
        UItext.text = string.Empty; // Clear the text after showing all messages
    }

    public IEnumerator showTurnMessage()
    {
        UItext.text = isPlayerTurn ? "Your Turn!" : "Enemy's Turn!";
        yield return new WaitForSeconds(messageDisplayTime);
        UItext.text = string.Empty; // Clear the text after the delay   
    }
}
