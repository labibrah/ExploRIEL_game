using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordButtonController : MonoBehaviour
{
    public TMP_Text label;
    private Button button;
    private SyntaxShuffleManager manager;
    public AudioSource clickSound;

    public void Init(string word, SyntaxShuffleManager mgr)
    {
        label.text = word;
        manager = mgr;
        button = GetComponent<Button>();
        clickSound = GetComponent<AudioSource>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        manager.OnWordClicked(this);
        clickSound.Play();
    }

    public string GetWord()
    {
        return label.text;
    }

    public void SetWord(string newWord)
    {
        label.text = newWord;
    }
}
