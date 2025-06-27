using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGame_WordOrder : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;
    public TextMeshProUGUI questionText;
    public Transform buttonArea;
    public Button wordButtonPrefab;
    public Button submitButton;
    public TextMeshProUGUI feedbackText;

    [Header("Questions")]
    public List<WordOrderQuestion> questionSet;

    private int currentQuestionIndex = 0;
    private List<Button> currentButtons = new List<Button>();
    private int firstSelectedIndex = -1;
    private bool answeredIncorrectly = false;
    private Action<bool> onComplete;

    public void Launch(List<WordOrderQuestion> questions, Action<bool> callback)
    {
        questionSet = questions;
        currentQuestionIndex = 0;
        answeredIncorrectly = false;
        onComplete = callback;

        panel.SetActive(true);
        feedbackText.text = "";
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        ClearButtons();
        WordOrderQuestion q = questionSet[currentQuestionIndex];
        questionText.text = q.prompt;

        string[] shuffledWords = (string[])q.correctOrder.Clone();
        ShuffleArray(shuffledWords);
        submitButton.gameObject.SetActive(true);

        for (int i = 0; i < shuffledWords.Length; i++)
        {
            int index = i;
            Button btn = Instantiate(wordButtonPrefab, buttonArea);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = shuffledWords[i];
            btn.onClick.AddListener(() => OnWordClicked(index));
            currentButtons.Add(btn);
        }

        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(CheckAnswer);
    }

    private void OnWordClicked(int index)
    {
        if (firstSelectedIndex == -1)
        {
            firstSelectedIndex = index;
            HighlightButton(index, true);
        }
        else
        {
            SwapText(firstSelectedIndex, index);
            HighlightButton(firstSelectedIndex, false);
            firstSelectedIndex = -1;
        }
    }

    private void SwapText(int a, int b)
    {
        var tmpA = currentButtons[a].GetComponentInChildren<TextMeshProUGUI>().text;
        currentButtons[a].GetComponentInChildren<TextMeshProUGUI>().text =
            currentButtons[b].GetComponentInChildren<TextMeshProUGUI>().text;
        currentButtons[b].GetComponentInChildren<TextMeshProUGUI>().text = tmpA;
    }

    private void HighlightButton(int index, bool highlight)
    {
        Color c = highlight ? Color.yellow : Color.white;
        currentButtons[index].GetComponent<Image>().color = c;
    }

    private void CheckAnswer()
    {
        string[] playerAnswer = new string[currentButtons.Count];
        for (int i = 0; i < currentButtons.Count; i++)
        {
            playerAnswer[i] = currentButtons[i].GetComponentInChildren<TextMeshProUGUI>().text;
        }

        WordOrderQuestion q = questionSet[currentQuestionIndex];
        bool correct = true;
        for (int i = 0; i < q.correctOrder.Length; i++)
        {
            if (playerAnswer[i] != q.correctOrder[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            feedbackText.text = "Correct!";
        }
        else
        {
            answeredIncorrectly = true;
            feedbackText.text = $"Incorrect.\n{q.explanation}";
        }

        StartCoroutine(NextQuestion());
    }

    private IEnumerator NextQuestion()
    {
        DisableButtons();
        yield return new WaitForSeconds(2.5f);

        currentQuestionIndex++;

        if (currentQuestionIndex < questionSet.Count)
        {
            ShowQuestion();
        }
        else
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        panel.SetActive(false);
        onComplete?.Invoke(!answeredIncorrectly);
    }

    private void ClearButtons()
    {
        foreach (Button b in currentButtons)
            Destroy(b.gameObject);
        currentButtons.Clear();
        firstSelectedIndex = -1;
    }

    private void DisableButtons()
    {
        foreach (Button b in currentButtons)
            b.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
    }

    private void ShuffleArray(string[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (array[k], array[n]) = (array[n], array[k]);
        }
    }
}
