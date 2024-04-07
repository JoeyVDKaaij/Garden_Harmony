using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionButtonScript : MonoBehaviour
{
    [Header("Action Settings")]
    [SerializeField, Tooltip("Set the action that this button performs.")]
    private DamageType action = DamageType.Bug;
    
    private Button _button;
    
    private void Awake()
    {
        GameManager.ActivatedAction += CheckActionsLeft;

        _button = GetComponent<Button>();
    }

    private void OnDestroy() { GameManager.ActivatedAction -= CheckActionsLeft; }

    public void ActivateAction()
    {
        if (action == DamageType.None)
            GameManager._instance.ActivatedActionEvent();
        else
            GameManager._instance.enableActionClicker(action);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) GameManager._instance.disableActionClicker();
    }

    private void CheckActionsLeft(int pActionsLeft)
    {
        _button.interactable = pActionsLeft > 0;
    }
}