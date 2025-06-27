using UnityEngine;

[CreateAssetMenu(fileName = "NewQuizQuestion", menuName = "Quiz/MultipleChoiceQuestion")]
public class MultipleChoiceQuestion : ScriptableObject
{
    [TextArea]
    public string question;
    public string[] choices;
    public int correctAnswerIndex; // 0-based index
    public string explanation; // Optional explanation for the correct answer
}
