using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossFightManager : MonoBehaviour
{
    [Header("Boss Settings")]
    public int bossMaxHP = 3;  // One HP per mini-game phase
    private int bossCurrentHP;

    [Header("UI Elements")]
    public TextMeshProUGUI bossHPText;
    public TextMeshProUGUI phaseFeedbackText;

    [Header("Mini-Game Managers")]
    public MiniGame_MultipleChoice multipleChoiceGame;
    public MiniGame_WordOrder wordOrderGame;
    public MiniGame_FeatureMatch featureMatchGame;

    [Header("Mini-Game Questions")]
    public List<MultipleChoiceQuestion> mcQuestions;
    public List<WordOrderQuestion> wordOrderQuestions;
    public List<FeatureMatchQuestion> featureMatchQuestions;

    private int currentPhase = 0;

    void Start()
    {
        bossCurrentHP = bossMaxHP;
        UpdateBossHPUI();
        StartCoroutine(StartNextPhase());
    }

    private IEnumerator StartNextPhase()
    {
        yield return new WaitForSeconds(1f);

        phaseFeedbackText.text = $"⚔️ Phase {currentPhase + 1} Starting...";

        yield return new WaitForSeconds(1.5f);

        switch (currentPhase)
        {
            case 0:
                multipleChoiceGame.LaunchQuestion(mcQuestions, OnMiniGameCompleted);
                break;
            case 1:
                wordOrderGame.Launch(wordOrderQuestions, OnMiniGameCompleted);
                break;
            case 2:
                featureMatchGame.Launch(featureMatchQuestions, OnMiniGameCompleted);
                break;
            default:
                Debug.Log("Battle complete!");
                BossDefeated();
                break;
        }
    }

    private void OnMiniGameCompleted(bool success)
    {
        if (success)
        {
            phaseFeedbackText.text = "✅ Phase Passed! Boss takes damage!";
            bossCurrentHP--;
            UpdateBossHPUI();
        }
        else
        {
            phaseFeedbackText.text = "❌ Phase Failed! Boss attacks you back!";
            // Optional: Call player.TakeDamage() if you have a player HP system
        }

        currentPhase++;

        if (bossCurrentHP > 0)
        {
            StartCoroutine(StartNextPhase());
        }
        else
        {
            BossDefeated();
        }
    }

    private void UpdateBossHPUI()
    {
        bossHPText.text = $"Boss HP: {bossCurrentHP}/{bossMaxHP}";
    }

    private void BossDefeated()
    {
        phaseFeedbackText.text = "🏆 Boss Defeated! Congratulations!";
        // TODO: Load ending scene or show reward UI
    }
}
