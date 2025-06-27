using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSculpture : Sign
{

    public int sculptureID;
    public bool isCompleted = false;
    public BoolValue sculptureCompletion;
    public List<MultipleChoiceQuestion> questionData;
    public List<WordOrderQuestion> wordOrderData;
    public List<FeatureMatchQuestion> featureMatchQuestions;
    public MiniGame_MultipleChoice quizManager;
    public MiniGame_WordOrder wordOrderManager;
    public MiniGame_FeatureMatch featureMatchManager;
    public enum ChallengeType
    {
        WordOrder,
        MultipleChoice,
        FeatureMatch
    }

    public ChallengeType challengeType;

    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (sculptureCompletion != null)
        {
            isCompleted = sculptureCompletion.runtimeValue;
        }
    }

    public override void Update()
    {
        if (isCompleted)
        {
            return;
        }
        if (dialogActive && Input.GetKeyDown(KeyCode.E))
        {
            if (audioSource != null && interactSound != null)
            {
                audioSource.PlayOneShot(interactSound);
            }

            if (!dialogBox.activeSelf)
            {
                dialogBox.SetActive(true);
                currentDialogIndex = 0;
                dialogText.text = dialogs.Length > 0 ? dialogs[currentDialogIndex] : "";
            }
            else
            {
                currentDialogIndex++;
                if (currentDialogIndex < dialogs.Length)
                {
                    dialogText.text = dialogs[currentDialogIndex];
                }
                else
                {
                    dialogBox.SetActive(false);
                    dialogActive = false;
                    currentDialogIndex = 0;
                    switch (challengeType)
                    {
                        case ChallengeType.WordOrder:
                            wordOrderManager.Launch(wordOrderData, OnMiniGameCompleted);
                            break;

                        case ChallengeType.MultipleChoice:
                            quizManager.LaunchQuestion(questionData, OnMiniGameCompleted);
                            break;
                        case ChallengeType.FeatureMatch:
                            featureMatchManager.Launch(featureMatchQuestions, OnMiniGameCompleted);
                            break;

                    }

                }
            }
        }
        else if (dialogActive && Input.GetKeyDown(KeyCode.Escape))
        {
            dialogBox.SetActive(false);
            dialogActive = false;
            currentDialogIndex = 0;
        }
    }

    void OnMiniGameCompleted(bool success)
    {
        if (success)
        {
            isCompleted = true;
            Debug.Log($"Sculpture {sculptureID} completed!");
            if (sculptureCompletion != null)
            {
                sculptureCompletion.runtimeValue = true;
            }
            interactSignal.Raise();
        }
        else
        {
            Debug.Log("Failed mini-game. Try again later.");
        }
    }
}
