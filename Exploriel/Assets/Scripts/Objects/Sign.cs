using TMPro;
using UnityEngine;

public class Sign : Interactable
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string[] dialogs;
    public bool dialogActive;
    public int currentDialogIndex = 0;
    public virtual void Update()
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
                    dialogBox.SetActive(false);
                    dialogActive = false;
                    currentDialogIndex = 0;
                }
            }
        }
        else if (dialogActive && Input.GetKeyDown(KeyCode.Escape))
        {
            dialogBox.SetActive(false);
            dialogActive = false;
            currentDialogIndex = 0;
        }
        else if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.SetActive(false);
            dialogActive = false;
            currentDialogIndex = 0;
        }
        else if (dialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            dialogBox.SetActive(false);
            dialogActive = false;
            currentDialogIndex = 0;
        }
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogActive = true;
            currentDialogIndex = 0;
            context.Raise();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogActive = false;
            dialogBox.SetActive(false);
            context.Raise();
            currentDialogIndex = 0;
        }
    }

}

