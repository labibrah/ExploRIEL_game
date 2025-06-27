using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplainationSpawner : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public Signal ExplanationRead;
    public bool explanationShown = false;
    public string explanationText;
    public AudioSource audioSource;
    public AudioClip explanationAudio;

    void Start()
    {
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

        if (ExplanationRead == null)
        {
            ExplanationRead = GetComponent<Signal>();
        }
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (explanationShown && Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.SetActive(false);
            ExplanationRead.Raise();
            explanationShown = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (audioSource != null && explanationAudio != null)
            {
                audioSource.PlayOneShot(explanationAudio);
            }
            if (dialogBox != null && dialogText != null)
            {
                dialogBox.SetActive(true);
                dialogText.text = explanationText;
                explanationShown = true;
            }
        }
    }
}
