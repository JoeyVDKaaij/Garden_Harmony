using UnityEngine;
using System;

[Serializable]
public class CursorSprites
{
    public Texture2D sprayCan;
    public Texture2D weedCutter;
    public Texture2D wateringCan;
    public Texture2D vertilizer;
}
public class GameManager : MonoBehaviour
{
    public static GameManager _instance {  get; private set; }
    
    [Header("Game Settings")]
    [SerializeField, Tooltip("Set how fast the game goes."), Min(0)]
    private float gameSpeed = 1f;
    
    [Header("Actions Settings")]
    [SerializeField, Tooltip("Set how fast the game goes."), Min(0)]
    private int maxAmountOfActions = 2;
    [SerializeField, Tooltip("Set the sprite of the cursor.")]
    private CursorSprites cursorSprite;
    
    private float _fixedDeltaTime;

    public static event Action<int> ActivatedAction;

    private int _actionsLeft;

    private bool _currentlyInAction = false;
    private DamageType _actionType = DamageType.None;
    
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

    public void enableActionClicker(DamageType pdamageType)
    {
        _actionType = pdamageType;
        switch (_actionType)
        {
            case DamageType.Bug:
                Cursor.SetCursor(cursorSprite.sprayCan, new Vector2(104,0), CursorMode.ForceSoftware);
                break;
            case DamageType.Weed:
                Cursor.SetCursor(cursorSprite.weedCutter, new Vector2(25,228), CursorMode.ForceSoftware);
                break;
            case DamageType.Thirsty:
                Cursor.SetCursor(cursorSprite.wateringCan, new Vector2(4,225), CursorMode.ForceSoftware);
                break;
            case DamageType.Eaten:
                Cursor.SetCursor(cursorSprite.vertilizer, new Vector2(138,104), CursorMode.ForceSoftware);
                break;
        }
    }

    public void disableActionClicker()
    {
        _actionType = DamageType.None;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public int ActionsLeft
    {
        get { return _actionsLeft; }
    }
    
    public bool CurrentlyInAction
    {
        get { return _currentlyInAction; }
    }
    
    public DamageType ActionType
    {
        get { return _actionType; }
    }
}
