using TMPro;
using UnityEngine;

public class DifficultyOptionsSetupScript : MonoBehaviour
{
    [Header("Difficulty Options Setup Settings")]
    [SerializeField, Tooltip("Set all the difficulty options in the game.")]
    private DifficultyScriptableObject[] difficulties = null;
    [SerializeField, Tooltip("Set all the difficulty options in the game.")]
    private GameObject difficultyOption = null;
    [SerializeField, Tooltip("Set all the difficulty options in the game.")]
    private TMP_Text difficultyDescriptionText = null;
    
    void Start()
    {
        if (difficulties != null && difficultyOption)
        {
            foreach (DifficultyScriptableObject difficulty in difficulties)
            {
                DifficultyButtonScript script = Instantiate(difficultyOption, transform).GetComponent<DifficultyButtonScript>();
                script.difficulty = difficulty;
                if (difficultyDescriptionText != null)
                    script.difficultyDescriptionText = difficultyDescriptionText;
            }
        }
    }
}
