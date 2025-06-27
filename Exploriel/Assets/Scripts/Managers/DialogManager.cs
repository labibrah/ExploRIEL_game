using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    private Queue<string> dialogLines = new Queue<string>();
    private Action onDialogComplete;
    private bool isDialogActive = false;

    void Start()
    {
        dialogBox.SetActive(false);
    }

    public void StartDialog(List<string> lines, Action onComplete = null)
    {
        dialogLines.Clear();
        foreach (string line in lines)
        {
            dialogLines.Enqueue(line);
        }

        onDialogComplete = onComplete;
        isDialogActive = true;
        dialogBox.SetActive(true);
        DisplayNextLine();
    }

    void Update()
    {
        if (isDialogActive && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }

    private void DisplayNextLine()
    {
        if (dialogLines.Count == 0)
        {
            EndDialog();
            return;
        }

        string currentLine = dialogLines.Dequeue();
        dialogText.text = currentLine;
    }

    private void EndDialog()
    {
        dialogBox.SetActive(false);
        isDialogActive = false;
        onDialogComplete?.Invoke();
    }
}
