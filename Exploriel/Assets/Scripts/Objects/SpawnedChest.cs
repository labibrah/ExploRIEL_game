using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedChest : Chest
{
    public override IEnumerator OpenChest()
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
        storedOpenState.runtimeValue = false;
        Destroy(gameObject);
    }
}
