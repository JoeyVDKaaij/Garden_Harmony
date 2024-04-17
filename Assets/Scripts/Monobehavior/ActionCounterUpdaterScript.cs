using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ActionCounterUpdaterScript : MonoBehaviour
{
    private TMP_Text _text;

    [SerializeField, Tooltip("Set to true if this should count the days survived.")]
    private bool daysSurvivedText = false;
    

    private void Awake()
    {
        GameManager.ActivatedAction += CounterUpdater;

        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (_text != null)
        {
            if (!daysSurvivedText)
                _text.SetText("Actions left: " + GameManager._instance.ActionsLeft);
            else 
                _text.SetText("Days survived: " + GameManager._instance.DaysSurvived);
        }
    }

    private void OnDestroy() { GameManager.ActivatedAction += CounterUpdater; }

    private void CounterUpdater(int pActionsLeft)
    {
        if (!daysSurvivedText) _text.SetText("Actions left: " + pActionsLeft);
        else if (pActionsLeft == 0)
        {
            GameManager._instance.UpdateDay();
            _text.SetText("Days survived: " + GameManager._instance.DaysSurvived);
        }
    }
}
