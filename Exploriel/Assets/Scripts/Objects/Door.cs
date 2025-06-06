using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        isOpen = false;

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
            doorSpriteRenderer.enabled = false; // Hide the door sprite
            doorCollider.enabled = false; // Disable the collider to allow passage
            Debug.Log("Door opened.");
        }
    }

    private bool HasRequiredKey()
    {
        // Check if the player has the required key in their inventory
        return playerInventory.hasItem(requiredKey);
    }
}
