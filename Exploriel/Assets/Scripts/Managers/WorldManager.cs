using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject[] regionDoors;
    public BoolValue[] regionDoorStates; // Array to hold the state of each region door
    public Signal regionDoorSignal;

    private void Start()
    {
        // Initialize region doors based on their states
        for (int i = 0; i < regionDoors.Length; i++)
        {
            if (regionDoorStates[i].GetValue())
            {
                OpenRegionDoor(i); // Open the door if the state is true
            }
            else
            {
                CloseRegionDoor(i); // Close the door if the state is false
            }
        }
    }


    public void OpenRegionDoor(int regionIndex)
    {
        if (regionIndex < 0 || regionIndex >= regionDoors.Length)
        {
            Debug.LogError("Invalid region index: " + regionIndex);
        }

        GameObject door = regionDoors[regionIndex];
        if (door != null)
        {
            //door.GetComponent<SpriteRenderer>().enabled = false; // Hide the door
            door.GetComponent<Animator>().SetBool("isOpen", true); // Trigger the open animation
            door.GetComponent<Collider2D>().enabled = false; // Disable the collider
            regionDoorStates[regionIndex].SetValue(true); // Update the state to open
            if (regionDoorSignal != null)
            {
                regionDoorSignal.Raise(); // Raise the signal to notify other systems
            }
            Debug.Log("Opened region door: " + door.name);
        }
        else
        {
            Debug.LogError("Region door not found at index: " + regionIndex);
        }
    }

    public void CloseRegionDoor(int regionIndex)
    {
        if (regionIndex < 0 || regionIndex >= regionDoors.Length)
        {
            Debug.LogError("Invalid region index: " + regionIndex);
        }

        GameObject door = regionDoors[regionIndex];
        if (door != null)
        {
            regionDoorStates[regionIndex].SetValue(false); // Update the state to closed
            door.GetComponent<SpriteRenderer>().enabled = true; // Show the door
            door.GetComponent<Collider2D>().enabled = true; // Enable the collider
            if (regionDoorSignal != null)
            {
                regionDoorSignal.Raise(); // Raise the signal to notify other systems
            }
            Debug.Log("Closed region door: " + door.name);
        }
        else
        {
            Debug.LogError("Region door not found at index: " + regionIndex);
        }
    }
}
