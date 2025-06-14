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

    public IEnumerator OpenChest()
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
