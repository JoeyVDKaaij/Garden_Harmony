using UnityEngine;

public class TogglePauseMenuScript : MonoBehaviour
{
    [Header("Pause Menu Toggle Settings")]
    [SerializeField, Tooltip("Set the pause menu that the player can interact with.")]
    private GameObject pauseMenu = null;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
            pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
