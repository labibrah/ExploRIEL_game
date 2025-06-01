using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public Signal contextOn;
    public Signal contextOff;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogActive = true;
            contextOn.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogActive = false;
            dialogBox.SetActive(false);
            contextOff.Raise();
        }
    }
}
