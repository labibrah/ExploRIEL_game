using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : Interactable
{
    public bool active;
    public BoolValue switchValue;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public SpriteRenderer spriteRenderer;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public Door door; // Reference to the door that this switch controls
    public string dialog;
    public bool dialogActive;
    private bool dialogShown = false; // Track if the dialog has been shown
    // Start is called before the first frame update
    void Start()
    {
        active = switchValue.initialValue; // Initialize the switch state from the scriptable object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.E))
        {
            if (active)
            {
                if (!dialogShown)
                {
                    dialogText.text = dialog;
                    dialogBox.SetActive(true);
                    dialogShown = true; // Mark that the dialog has been shown
                }
                else
                {
                    //ShowingPuzzle();
                    door.OpenDoor();
                    active = false; // Set the switch to inactive after activation
                    spriteRenderer.sprite = inactiveSprite; // Change the sprite to inactive
                    dialogBox.SetActive(false); // Optionally hide the dialog
                    dialogShown = false; // Reset for next interaction
                }
            }
        }

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogActive = true;
            context.Raise();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogActive = false;
            dialogBox.SetActive(false);
            dialogShown = false; // Reset dialogShown when player exits
            context.Raise();
        }
    }

    private void ShowingPuzzle()
    {
        dialogBox.SetActive(true);
        dialogText.text = dialog;
    }

}
