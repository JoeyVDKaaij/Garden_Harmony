using UnityEngine;
using TMPro;

public class EndlessToggleScript : MonoBehaviour
{
    [SerializeField, Tooltip("Set the description of this toggle."), TextArea]
    private string toggleDescription = "";
    [SerializeField, Tooltip("Set the description text GameObject that changes when hovering over this toggle.")]
    private TMP_Text difficultyDescriptionText = null;
    
    public void EnableDescription()
    {
        if (difficultyDescriptionText != null)
        {
            difficultyDescriptionText.gameObject.SetActive(true);
            difficultyDescriptionText.SetText(toggleDescription);
        }
    }

    public void DisableDescription()
    {
        if (difficultyDescriptionText != null)
            difficultyDescriptionText.gameObject.SetActive(false);
    }

    public void ToggleEndless()
    {
        GameManager._instance.endless = !GameManager._instance.endless;
    }
}
