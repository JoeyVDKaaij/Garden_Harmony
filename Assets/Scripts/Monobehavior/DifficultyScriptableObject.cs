using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "Difficulty")]
public class DifficultyScriptableObject : ScriptableObject
{
    [Header("UI Settings")]
    [Tooltip("The description of the difficulty."), TextArea]
    public string description = "";

    [Header("Game Settings")] 
    [Tooltip("Set the max amount of moves per day."), Min(1)]
    public int maxAmountOfMoves = 1;
    [Tooltip("Set the needed consecutive amount of days where no plants die before the good ending."), Min(1)]
    public int neededPlantsSavedStreak = 1;
    [Tooltip("Set how many plants minimum gets affected each night."), Min(1)]
    public int affectedPlantMinimum = 1;
    [Tooltip("Set the max amount of plants that can get affected."), Min(2)]
    public int affectedPlantLimit = 2;
    [Tooltip("Set how many nights it takes until a plant dies."), Min(2)]
    public int maxNightCyclesUntilDeath = 2;
    [Tooltip("Set how many plants need to be left to get a game over."), Min(0)]
    public int minimumPlantCount = 3;
}
