using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MagicStone : Sign
{
    public GameObject WizardQuizManager;

    public override void Update()
    {
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
                    //dialogBox.SetActive(false);
                    dialogActive = false;
                    currentDialogIndex = 0;
                    WizardQuizManager.GetComponent<WizardQuizManager>().StartQuizDialogue();
                }
            }
        }
    }
}

