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

    private int _nightCyclesUntilDeath;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
                _plantSpriteId = Random.Range(0, plantSprites.Length - 1);
                if (plantSprites[_plantSpriteId].normal != null)
                    sr.sprite = plantSprites[_plantSpriteId].normal;
            }
        }
    }

    public void StartDamagingPlant()
    {
        int damageTypeId = Random.Range(1, Enum.GetValues(typeof(DamageType)).Length);
        damageType = (DamageType)damageTypeId;
        if (plantSprites != null && plantSprites.Length != 0)
        { 
            if (plantSprites[_plantSpriteId].CheckValues())
            {
                switch (damageTypeId)
                {
                    case 1:
                        sr.sprite = plantSprites[_plantSpriteId].bugsEating;
                        break;
                    case 2:
                        sr.sprite = plantSprites[_plantSpriteId].weed;
                        break;
                    case 3:
                        sr.sprite = plantSprites[_plantSpriteId].thirsty;
                        break;
                    case 4:
                        sr.sprite = plantSprites[_plantSpriteId].eaten;
                        break;
                }
            }
        }
    }

    private void Update()
    {
        if (_nightCyclesUntilDeath <= 0)
        {
            Destroy(gameObject);
        }
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