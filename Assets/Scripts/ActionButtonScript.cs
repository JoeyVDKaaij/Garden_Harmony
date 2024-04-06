using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionButtonScript : MonoBehaviour
{
    [Header("Action Settings")]
    [SerializeField, Tooltip("Set the action that this button performs.")]
    private Actions action = Actions.Spray;
    
    private Button _button;
    
    private void Awake()
    {
        GameManager.ActivatedAction += CheckActionsLeft;

        _button = GetComponent<Button>();
    }

    private void OnDestroy() { GameManager.ActivatedAction -= CheckActionsLeft; }

    public void ActivateAction()
    {
        GameManager._instance.ActivatedActionEvent();
        
        Debug.Log(action);
    }

    private void CheckActionsLeft(int pActionsLeft)
    {
        _button.interactable = pActionsLeft > 0;
    }
}

public enum Actions
{
    Spray,
    Soil,
    Water,
    Weed,
    PowerUp
}