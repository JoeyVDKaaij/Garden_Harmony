using UnityEngine;
using TMPro;

public class DifficultyButtonScript : MonoBehaviour
{
    [HideInInspector, Tooltip("Set the difficulty scriptable objects.")]
    public DifficultyScriptableObject difficulty;
    
    [HideInInspector, Tooltip("Set the description text GameObject that changes when hovering over this button.")]
    public TMP_Text difficultyDescriptionText = null;

    private void Start()
    {
        if (difficulty != null)
        {
            transform.GetChild(0).GetComponent<TMP_Text>().SetText(difficulty.name);
        }
    }

    public void EnableDescription()
    {
        if (difficultyDescriptionText != null && difficulty != null)
        {
            difficultyDescriptionText.gameObject.SetActive(true);
            difficultyDescriptionText.SetText(difficulty.description);
        }
    }

    public void DisableDescription()
    {
        if (difficultyDescriptionText != null)
            difficultyDescriptionText.gameObject.SetActive(false);
    }

    public void SelectDifficulty()
    {
        GameManager._instance.difficulty = difficulty;
        GameManager._instance.ResetValues();
        SceneSwitch._instance.scene_changer(2);
    }
}