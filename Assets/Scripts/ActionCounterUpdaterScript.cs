using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ActionCounterUpdaterScript : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        GameManager.ActivatedAction += CounterUpdater;

        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (_text != null)
            _text.SetText("Actions left: " + GameManager._instance.ActionsLeft);
    }

    private void OnDestroy() { GameManager.ActivatedAction += CounterUpdater; }

    private void CounterUpdater(int pActionsLeft)
    {
        _text.SetText("Actions left: " + pActionsLeft);
    }
}
