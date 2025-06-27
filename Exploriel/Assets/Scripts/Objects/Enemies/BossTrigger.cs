using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public List<string> bossDialogLines;
    public DialogManager dialogManager;  // Reference to your dialog manager
    public string bossFightSceneName = "BossFightingScene";  // Name of your boss fight scene

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            dialogManager.StartDialog(bossDialogLines, OnDialogComplete);
        }
    }

    private void OnDialogComplete()
    {
        // After dialog finishes, load the fight scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(bossFightSceneName);
    }
}
