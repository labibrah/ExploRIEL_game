using UnityEngine;

[CreateAssetMenu(fileName = "NewFeatureMatchQuestion", menuName = "Quiz/FeatureMatchQuestion")]
public class FeatureMatchQuestion : ScriptableObject
{
    public string[] languages;
    public string[] features;  // Example: ["SVO", "SOV", "VSO", "OSV"]
    public int[] correctFeatureIndices;  // Each index corresponds to languages[i]'s correct feature
    [TextArea]
    public string explanation;  // Optional explanation for wrong answers
}
