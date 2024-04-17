using System;
using UnityEngine;
using TMPro;

public class SetupLeaderboardScript : MonoBehaviour
{
    [Header("Difficulty Options Setup Settings")]
    [SerializeField, Tooltip("Set all the difficulty options in the game.")]
    private DifficultyScriptableObject[] difficulties = null;

    private TMP_Text _text;

    private bool _endless;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        SetupText();
    }

    private void SetupText()
    {
        _text.SetText("");
        ScoreData data = SaveSystem.LoadScore();
        if (difficulties != null)
        {
            foreach (DifficultyScriptableObject difficulty in difficulties)
            {
                int score = 0;
                if (data != null)
                {
                    foreach ((string, int) difficultyData in data.difficultyData)
                    {
                        if ($"{difficulty.name}{(_endless?" (Endless)":"")}" == difficultyData.Item1) score = difficultyData.Item2;
                    }
                }

                string scoreText;
                if (score == -1)
                    scoreText = "You completed the";
                else if (score == 0)
                    scoreText = "You survived 0 days in";
                else
                    scoreText = $"You survived {score} days in";
                
                _text.SetText($"{_text.text}\n {scoreText} {difficulty.name} difficulty {(_endless?" (Endless)":"")}");
            }
        }
    }

    public void ToggleEndlessScore()
    {
        _endless = !_endless;
        SetupText();
    }
}
