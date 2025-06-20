using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DoorType
{
    Normal,
    Locked,
    Secret,
    Dangerous
}

public class Door : Interactable
{
    [Header("Door Settings")]
    public DoorType doorType;
    public bool isOpen;
    public Inventory playerInventory;
    public SpriteRenderer doorSpriteRenderer;
    public BoxCollider2D doorCollider;
    public Item requiredKey;
    public BoolValue Opened;
    public Sprite OpenedSprite;
    public Sprite ClosedSprite;

    public override void Start()
    {
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
        // Get the BoxCollider2D that is not set as trigger
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                doorCollider = col;
                break;
            }
        }
        audioSource = GetComponent<AudioSource>();
        isOpen = Opened.runtimeValue;

        // Set the initial sprite based on the door's state
        if (isOpen)
        {
            doorSpriteRenderer.sprite = OpenedSprite;
            doorCollider.enabled = false; // Disable collider when the door is open
        }
        else
        {
            doorSpriteRenderer.sprite = ClosedSprite;
            doorCollider.enabled = true; // Enable collider when the door is closed
        }

        // Set initial state based on door type
        switch (doorType)
        {
            case DoorType.Locked:
                // Logic for locked doors, e.g., require a key
                break;
            case DoorType.Secret:
                // Logic for secret doors, e.g., hidden or special interaction
                break;
            case DoorType.Dangerous:
                // Logic for dangerous doors, e.g., traps or hazards
                break;
            default:
                // Normal door behavior
                break;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (doorType == DoorType.Locked)
            {
                if (HasRequiredKey())
                {
                    OpenDoor();
                }
                else
                {
                    Debug.Log("The door is locked. You need a key to open it.");
                }
            }
            else
            {
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Opened.runtimeValue = true; // Update the opened state
            doorSpriteRenderer.sprite = OpenedSprite; // Change to opened sprite
            doorCollider.enabled = false; // Disable the collider to allow passage
            if (audioSource != null && interactSound != null)
            {
                audioSource.PlayOneShot(interactSound); // Play the interaction sound
            }
            if (interactSignal != null)
            {
                interactSignal.Raise(); // Raise the interaction signal
            }
            Debug.Log("Door opened.");
        }
    }

    private bool HasRequiredKey()
    {
        // Check if the player has the required key in their inventory
        return playerInventory.HasItem(requiredKey);
    }
}
