using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    public Item contents;
    public bool isOpen = false;
    public BoolValue storedOpenState;
    public Signal raiseItemSignal;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public Animator animator;
    public Inventory inventory;

    public override void Start()
    {
        animator = GetComponent<Animator>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (storedOpenState != null)
        {
            isOpen = storedOpenState.runtimeValue;
            animator.SetBool("isOpen", isOpen);
        }
        else
        {
            Debug.LogWarning("Stored Open State is not assigned in Chest.");
        }

        if (dialogBox == null)
        {
            // Find the Canvas in the scene
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                // Find the inactive dialogBox by name or type in the Canvas hierarchy
                dialogBox = canvas.transform.Find("DialogBox")?.gameObject;
            }
        }

        if (dialogText == null)
        {
            if (dialogBox != null)
            {
                dialogText = dialogBox.GetComponentInChildren<TextMeshProUGUI>(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!isOpen)
            {
                StartCoroutine(OpenChest());
            }
            else
            {
                StartCoroutine(ChestOpened());
            }
        }
    }

    public virtual IEnumerator OpenChest()
    {
        isOpen = true;
        if (audioSource != null && interactSound != null)
        {
            audioSource.PlayOneShot(interactSound);
        }
        storedOpenState.runtimeValue = isOpen;
        animator.SetBool("isOpen", true);
        dialogBox.SetActive(true);
        dialogText.text = contents.description;
        inventory.AddItem(contents);
        raiseItemSignal.Raise();
        context.Raise();
        interactSignal.Raise();
        yield return new WaitForSeconds(2f);
        context.Raise();
        dialogBox.SetActive(false);
        raiseItemSignal.Raise();

    }

    public IEnumerator ChestOpened()
    {
        dialogBox.SetActive(true);
        dialogText.text = "The chest is already open.";
        yield return new WaitForSeconds(2f);
        dialogBox.SetActive(false);
    }
}
