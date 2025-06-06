using TMPro;
using UnityEngine;

public class Sign : Interactable
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string dialog;
    public bool dialogActive;

    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.SetActive(!dialogBox.activeSelf);
            dialogText.text = dialog;
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
            context.Raise();
        }
    }
}
