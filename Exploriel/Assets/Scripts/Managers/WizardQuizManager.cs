using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class WizardQuizManager : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public GameObject MagicWallPrefab; // optional reference to spawn a magic wall
    public List<QuizStatement> quizStatements = new List<QuizStatement>();
    public GameObject keyPieceTriggerPrefab;
    public GameObject explainationTriggerPrefab;
    public Transform trueZoneSpawnPoint;   // up
    public Transform falseZoneSpawnPoint;  // down
    public BoxCollider2D quizTrigger; // optional reference to trigger the quiz
    public GameObject SceneTransitionPrefab; // optional reference to scene transition
    public Transform player;

    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private bool isTalking = false;
    private bool waitingForChoice = false;
    private bool sceneLocked = false;

    public Inventory inventory;
    public GameObject lockedDoor; // optional reference to open it


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        quizTrigger = GetComponent<BoxCollider2D>();
        quizTrigger.enabled = false; // disable the trigger until the quiz starts
    }

    public void StartQuizDialogue()
    {
        MagicWallPrefab.SetActive(true);
        currentQuestionIndex = 0;
        correctAnswers = 0;
        StartCoroutine(ShowQuizStatement(currentQuestionIndex));
    }

    public IEnumerator ShowQuizStatement(int index)
    {
        if (index < 0 || index >= quizStatements.Count) yield break;

        dialogBox.SetActive(true);
        dialogText.text = quizStatements[index].text;

        yield return new WaitForSeconds(1.5f); // wait before allowing interaction
        isTalking = true;
    }

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.SetActive(false);
            isTalking = false;
            SpawnTriggersForCurrentQuestion();
        }
    }

    void SpawnTriggersForCurrentQuestion()
    {
        if (currentQuestionIndex >= quizStatements.Count || sceneLocked) return;

        waitingForChoice = true;
        bool isTrue = quizStatements[currentQuestionIndex].isTrue;

        if (isTrue)
        {
            Instantiate(keyPieceTriggerPrefab, trueZoneSpawnPoint.position, Quaternion.identity);
            Instantiate(explainationTriggerPrefab, falseZoneSpawnPoint.position, Quaternion.identity);
            explainationTriggerPrefab.GetComponent<ExplainationSpawner>().explanationText = quizStatements[currentQuestionIndex].explanation;
            Debug.Log("True statement: " + quizStatements[currentQuestionIndex].text);
        }
        else
        {
            Instantiate(keyPieceTriggerPrefab, falseZoneSpawnPoint.position, Quaternion.identity);
            Instantiate(explainationTriggerPrefab, trueZoneSpawnPoint.position, Quaternion.identity);
            explainationTriggerPrefab.GetComponent<ExplainationSpawner>().explanationText = quizStatements[currentQuestionIndex].explanation;
        }
    }

    public void HandlePlayerChoice(bool isCorrect)
    {
        if (!waitingForChoice) return;

        waitingForChoice = false;

        if (isCorrect)
        {
            correctAnswers++;
            Debug.Log("Correct! Gained key piece.");
        }
        else
        {
            Debug.Log("Wrong! You will fight.");
            // Enemy already spawned
        }

        currentQuestionIndex++;

        if (correctAnswers >= 3)
        {
            UnlockDoor();
        }
        else if (currentQuestionIndex < quizStatements.Count)
        {
            // Next dialogue
            quizTrigger.enabled = true;
        }
        else
        {
            Debug.Log("It seems you need more practice.");
            Instantiate(SceneTransitionPrefab, player.position, Quaternion.identity);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(WaitAndShowNextStatement());
    }

    IEnumerator WaitAndShowNextStatement()
    {
        quizTrigger.enabled = false; // disable the trigger to prevent multiple activations
        yield return new WaitForSeconds(3f); // allow time to finish fight or reward
        dialogBox.SetActive(true);
        dialogText.text = quizStatements[currentQuestionIndex].text;
        isTalking = true;
    }

    void UnlockDoor()
    {
        sceneLocked = true;
        if (lockedDoor != null)
        {
            lockedDoor.SetActive(false);
        }

        Debug.Log("Door unlocked! You may pass.");
    }
}


[System.Serializable]
public class QuizStatement
{
    public string text;
    public bool isTrue;
    public string explanation; // optional explanation for the answer
}