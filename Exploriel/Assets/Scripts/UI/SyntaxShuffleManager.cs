using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SyntaxShuffleManager : MonoBehaviour
{
    public GameObject wordButtonPrefab;
    public Transform wordSlotsContainer;

    private List<string> correctSentence;
    private List<WordButtonController> currentButtons = new();
    public float duration = 30f; // Time limit for the challenge
    private float timeRemaining;
    private bool timerRunning = false;
    public TextMeshProUGUI timerText; // UI element to display the timer

    private WordButtonController firstSelected;
    private BattleManager battleManager;
    private bool answered = false;

    void Start()
    {
        LoadSentence("I want to eat pizza");
    }

    public void LoadSentence(string sentence)
    {
        answered = false; // Reset answered state for new challenge
        correctSentence = new List<string>(sentence.Split(' '));
        List<string> shuffled = new List<string>(correctSentence);
        Shuffle(shuffled);

        foreach (Transform child in wordSlotsContainer)
            Destroy(child.gameObject);

        currentButtons.Clear();
        firstSelected = null;

        foreach (var word in shuffled)
        {
            GameObject btn = Instantiate(wordButtonPrefab, wordSlotsContainer);
            var controller = btn.GetComponent<WordButtonController>();
            controller.Init(word, this);
            currentButtons.Add(controller);
        }
    }

    public void OnWordClicked(WordButtonController clicked)
    {
        if (firstSelected == null)
        {
            firstSelected = clicked;
            clicked.GetComponent<Image>().color = Color.yellow; // Highlight
        }
        else
        {
            SwapWords(firstSelected, clicked);
            firstSelected.GetComponent<Image>().color = Color.white;
            firstSelected = null;
        }
    }

    private void SwapWords(WordButtonController a, WordButtonController b)
    {
        string temp = a.GetWord();
        a.SetWord(b.GetWord());
        b.SetWord(temp);
    }

    public void SubmitAnswer()
    {
        timerText.gameObject.SetActive(false); // Hide timer when submitting
        if (answered) return; // Prevent double submit
        answered = true;

        bool correct = CheckAnswer();
        battleManager.OnPlayerSubmitted(correct);
    }

    private bool CheckAnswer()
    {
        for (int i = 0; i < correctSentence.Count; i++)
        {
            if (currentButtons[i].GetWord() != correctSentence[i])
                return false;
        }
        return true;
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void StartNewChallenge(BattleManager bm)
    {
        battleManager = bm;
        LoadSentence("I want to eat pizza"); // Load random or level-based
        timerText.gameObject.SetActive(true);
        StartCoroutine(StartTimer());

    }


    private IEnumerator StartTimer()
    {
        timeRemaining = duration;

        while (timeRemaining > 0 && !answered)
        {
            yield return null;
            timeRemaining -= Time.deltaTime;
            timerText.text = $"Time Remaining: {timeRemaining:F1}"; // Update timer UI

            // Color changes
            if (timeRemaining > duration * 0.6f)
                timerText.color = Color.green;
            else if (timeRemaining > duration * 0.3f)
                timerText.color = Color.yellow;
            else
                timerText.color = Color.red;

            // Optional: Pulse or flash when under 3 seconds
            if (timeRemaining < 3f)
            {
                float scale = 1.1f + Mathf.PingPong(Time.time * 5f, 0.2f); // subtle pulse
                timerText.transform.localScale = new Vector3(scale, scale, 1f);
            }
            else
            {
                timerText.transform.localScale = Vector3.one;
            }
        }

        if (!answered)
        {
            Debug.Log("Time's up!");
            timerText.gameObject.SetActive(false);
            battleManager.OnPlayerSubmitted(false);
        }
    }
}
