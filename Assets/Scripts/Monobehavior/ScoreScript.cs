using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ScoreScript : MonoBehaviour
{
    [Header("Debug Settings")]
    [SerializeField, Tooltip("Enables the good ending on default.")]
    private bool testGoodEnding = false;
    
    private void Start()
    {
        if (GameManager._instance.SavedPlantsEnding || testGoodEnding)
        {
            GetComponent<TMP_Text>().SetText("Your managed to give your plants a healthy environment!!!");
            if (testGoodEnding) Debug.LogWarning("Debug option \"Test Good Ending\" is on!");
            SaveSystem.SaveScore(GameManager._instance.difficulty.name, -1);
        }
        else
        {
            GetComponent<TMP_Text>().SetText($"Your plants survived: {GameManager._instance.DaysSurvived} DAYS!!!");
            if (GameManager._instance.endless)
                SaveSystem.SaveScore($"{GameManager._instance.difficulty.name} (Endless)", GameManager._instance.DaysSurvived);
            else
                SaveSystem.SaveScore(GameManager._instance.difficulty.name, GameManager._instance.DaysSurvived);
        }
    }
}
