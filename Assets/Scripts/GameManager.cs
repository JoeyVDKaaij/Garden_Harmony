using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance {  get; private set; }
    
    [Header("Game Settings")]
    [SerializeField, Tooltip("Set how fast the game goes."), Min(0)]
    private float gameSpeed = 1f;
    
    [Header("Actions Settings")]
    [SerializeField, Tooltip("Set how fast the game goes."), Min(0)]
    private int maxAmountOfActions = 2;
    
    private float _fixedDeltaTime;

    public static event Action<int> ActivatedAction;

    private int _actionsLeft;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            if (transform.parent.gameObject != null) DontDestroyOnLoad(transform.parent.gameObject);
            else DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _fixedDeltaTime = Time.fixedDeltaTime;

        _actionsLeft = maxAmountOfActions;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set the game speed depending on the gameSpeed value
        if (Time.timeScale != gameSpeed)
        {
            Time.timeScale = gameSpeed;
            // Adjust fixed delta time according to timescale
            Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
        }
    }

    public void ActivatedActionEvent()
    {
        _actionsLeft--;
        ActivatedAction?.Invoke(_actionsLeft);
    }

    public void ResetActions()
    {
        _actionsLeft = maxAmountOfActions;
        ActivatedAction?.Invoke(_actionsLeft);
    }
    
    public int ActionsLeft
    {
        get { return _actionsLeft; }
    }
}
