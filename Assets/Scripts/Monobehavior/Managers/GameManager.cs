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
    [Tooltip("Set to false if the gameplay settings should be set using what is set in the GameManager instead of from the difficulty scriptable objects."), Min(0)]
    public bool setValuesUsingDifficulties = true;
    [SerializeField, Tooltip("Set how fast the game goes."), Min(1)]
    private int neededPlantsSavedStreak = 10;
    
    
    [Header("Actions Settings")]
    [SerializeField, Tooltip("Set how fast the game goes."), Min(0)]
    private int maxAmountOfActions = 2;
    [SerializeField, Tooltip("Set the sprite of the cursor.")]
    private CursorSprites cursorSprite;
    
    private float _fixedDeltaTime;

    [HideInInspector]
    public DifficultyScriptableObject difficulty;

    public static event Action<int> ActivatedAction;

    private int _actionsLeft;
    private int _daysSurvivedCounter;

    private bool _currentlyInAction = false;
    private DamageType _actionType = DamageType.None;
    
    private int _currentPlantsSavedStreak = 0;
    private bool _savedPlantsEnding = false;

    [HideInInspector]
    public bool endless = false;
    
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

    public void ActivatedActionEvent(DamageType pDamageType)
    {
        if (pDamageType != DamageType.None)
            _actionsLeft--;
        else
            _actionsLeft = 0;
        ActivatedAction?.Invoke(_actionsLeft);
    }

    public void ResetActions()
    {
        _actionsLeft = maxAmountOfActions;
        ActivatedAction?.Invoke(_actionsLeft);
    }

    public void EnableActionClicker(DamageType pdamageType)
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

    public void DisableActionClicker()
    {
        _actionType = DamageType.None;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void UpdateDay()
    {
        _daysSurvivedCounter++;
        _currentPlantsSavedStreak++;
        if (_currentPlantsSavedStreak >= neededPlantsSavedStreak && !endless)
        {
            _savedPlantsEnding = true;
            SceneSwitch._instance.scene_changer(3);
        }
    }

    public void ResetValues()
    {
        if (setValuesUsingDifficulties && difficulty != null)
        {
            _actionsLeft = difficulty.maxAmountOfMoves;
            neededPlantsSavedStreak = difficulty.neededPlantsSavedStreak;
        }
        else
        {
            _actionsLeft = maxAmountOfActions;
        }
        _currentlyInAction = false;
        _currentPlantsSavedStreak = 0;
        _savedPlantsEnding = false;
        _actionType = DamageType.None;
        _daysSurvivedCounter = 0;
    }

    public void ResetPlantsSavedStreak()
    {
        _currentPlantsSavedStreak = 0;
    }
    
    public int ActionsLeft
    {
        get { return _actionsLeft; }
    }

    public int DaysSurvived
    {
        get { return _daysSurvivedCounter; }
    }
    
    public bool CurrentlyInAction
    {
        get { return _currentlyInAction; }
    }
    
    public DamageType ActionType
    {
        get { return _actionType; }
    }

    public bool SavedPlantsEnding
    {
        get { return _savedPlantsEnding; }
    }
}
