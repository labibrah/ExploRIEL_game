using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuPanel;
    public GameObject quizPanel;
    public GameObject dialoguePanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
    }

    void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        quizPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }
}
