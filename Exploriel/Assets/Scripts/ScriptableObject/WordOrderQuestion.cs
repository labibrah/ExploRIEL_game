using UnityEngine;

[CreateAssetMenu(fileName = "NewWordOrderQuestion", menuName = "Quiz/WordOrderQuestion")]
public class WordOrderQuestion : ScriptableObject
{
    [TextArea]
    public string prompt;  // Displayed on top
    public string[] correctOrder;
    [TextArea]
    public string explanation;  // Explanation shown on wrong answer
}
