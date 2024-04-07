using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PlantSprites
{
    [Tooltip("The sprite that shows when the plant is not in danger")]
    public Sprite normal;
    [Tooltip("The sprite that shows when the plant has bugs on it")]
    public Sprite bugsEating;
    [Tooltip("The sprite that shows when the plant has weed around it")]
    public Sprite weed;
    [Tooltip("The sprite that shows when the plant is thirsty")]
    public Sprite thirsty;
    [Tooltip("The sprite that shows when the plant has been eaten.")]
    public Sprite eaten;

    public bool CheckValues()
    {
        if (normal != null && bugsEating != null && weed != null && thirsty != null && eaten != null)
            return true;
        
        return false;
    }
}

[RequireComponent(typeof(SpriteRenderer))]
public class PlantHealthScript : MonoBehaviour
{
    private SpriteRenderer sr;
    
    [Header("Sprite Settings")]
    [SerializeField, Tooltip("The sprite of the plant.")]
    private PlantSprites[] plantSprites;

    private int _plantSpriteId;
    
    [Header("Health Settings")] 
    [SerializeField, Tooltip("How many nightcycles it takes until it dies.")]
    private int maxNightCyclesUntilDeath = 3;
    [SerializeField, Tooltip("Which damage state the plant is currently in.")]
    private DamageType damageType = DamageType.None;

    private GardenManagmentScript _gardenManagmentScript;

    private int _nightCyclesUntilDeath;

    private bool delay;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        _gardenManagmentScript = transform.parent.GetComponent<GardenManagmentScript>();
        _nightCyclesUntilDeath = maxNightCyclesUntilDeath;
    }

    private void Start()
    {
        if (plantSprites.Length < 1) Debug.LogError("There is no sprites assigned");
        else
        {
            for (int i = 0; i < plantSprites.Length; i++)
            {
                if (!plantSprites[i].CheckValues())
                    Debug.LogError("Element " + i + " of the Plant Sprites has a missing sprite in a Sprite Variable!");
            }

            if (plantSprites != null)
            {
                _plantSpriteId = Random.Range(0, plantSprites.Length);
                if (plantSprites[_plantSpriteId].normal != null)
                    sr.sprite = plantSprites[_plantSpriteId].normal;
            }
        }
    }

    public void StartDamagingPlant()
    {
        int damageTypeId = Random.Range(1, Enum.GetValues(typeof(DamageType)).Length);
        damageType = (DamageType)damageTypeId;
        Debug.Log("test");
        if (plantSprites != null && plantSprites.Length != 0)
        { 
            if (plantSprites[_plantSpriteId].CheckValues())
            {
                Debug.Log(damageType);
                switch (damageType)
                {
                    case DamageType.Bug:
                        sr.sprite = plantSprites[_plantSpriteId].bugsEating;
                        break;
                    case DamageType.Weed:
                        sr.sprite = plantSprites[_plantSpriteId].weed;
                        break;
                    case DamageType.Thirsty:
                        sr.sprite = plantSprites[_plantSpriteId].thirsty;
                        break;
                    case DamageType.Eaten:
                        sr.sprite = plantSprites[_plantSpriteId].eaten;
                        break;
                }

                delay = true;
            }
        }
    }

    private void Update()
    {
        if (_nightCyclesUntilDeath <= 0)
        {
            _gardenManagmentScript.RemovePlantFromList(gameObject, true);
        }

        if (Input.GetMouseButtonDown(0) && GameManager._instance.ActionType == damageType && damageType != DamageType.None)
        {
            FixPlant();
            GameManager._instance.disableActionClicker();
            GameManager._instance.ActivatedActionEvent();
        }

        delay = false;
    }

    public void DamagePlant()
    {
        _nightCyclesUntilDeath--;
    }

    private void FixPlant()
    {
        _nightCyclesUntilDeath = maxNightCyclesUntilDeath;
        if (plantSprites != null)
        {
            if (plantSprites[_plantSpriteId].normal != null)
                sr.sprite = plantSprites[_plantSpriteId].normal;
        }

        damageType = DamageType.None;
        _gardenManagmentScript.ToggleBetweenLists(gameObject);
    }
}

public enum DamageType
{
    None = 0,
    Bug = 1,
    Weed = 2,
    Thirsty = 3,
    Eaten = 4
}