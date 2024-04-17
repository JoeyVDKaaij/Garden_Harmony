using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GardenManagmentScript : MonoBehaviour
{
    private List<GameObject> _noEffectPlants = new List<GameObject>();
    private List<GameObject> _affectedPlants = new List<GameObject>();

    [Header("Garden Settings")]
    [SerializeField, Tooltip("Set how many plants minimum should affected with affects each night."), Min(0)]
    private int affectedPlantMinimum;
    [SerializeField, Tooltip("Set how many plants get affected with affects each night max."), Min(1)]
    private int affectedPlantLimit = 3;
    [SerializeField, Tooltip("Set how many plants should be alive before the game ends."), Min(0)]
    private int minimumPlantCount = 0;
    
    
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _noEffectPlants.Add(transform.GetChild(i).gameObject);
        }

        if (GameManager._instance.setValuesUsingDifficulties && GameManager._instance.difficulty != null)
        {
            affectedPlantMinimum = GameManager._instance.difficulty.affectedPlantMinimum;
            if (GameManager._instance.difficulty.affectedPlantLimit <= affectedPlantMinimum)
                affectedPlantLimit = affectedPlantMinimum + 1;
            else
                affectedPlantLimit = GameManager._instance.difficulty.affectedPlantLimit;
            minimumPlantCount = GameManager._instance.difficulty.minimumPlantCount;
        }
    }

    public void ChangeGarden()
    {
        int plantsAffected = Random.Range(affectedPlantMinimum, affectedPlantLimit);

        for (int i = 0; i < plantsAffected; i++)
        {
            if (_noEffectPlants.Count > 0)
            {
                int affectedPlant = Random.Range(0, _noEffectPlants.Count);
                _noEffectPlants[affectedPlant].GetComponent<PlantHealthScript>().StartDamagingPlant();
                _affectedPlants.Add(_noEffectPlants[affectedPlant]);
                _noEffectPlants.Remove(_noEffectPlants[affectedPlant]);
            }
        }

        for (int i = _affectedPlants.Count-1; i > -1; --i)
        {
            if (_affectedPlants[i] != null)
            {
                _affectedPlants[i].GetComponent<PlantHealthScript>().DamagePlant();
            }
            else _affectedPlants.Remove(_affectedPlants[i]);
        }
    }

    public void RemovePlantFromList(GameObject pPlant, bool affected)
    {
        if (pPlant != null)
        {
            if (affected) _affectedPlants.Remove(pPlant);
            else _noEffectPlants.Remove(pPlant);
            Destroy(pPlant);
            GameManager._instance.ResetPlantsSavedStreak();
        }
    }

    public void ToggleBetweenLists(GameObject pPlant)
    {
        if (_noEffectPlants.Count <= 0)
        {
            bool objectSpotted = false;
            foreach (GameObject obj in _noEffectPlants)
            {
                if (obj == pPlant) objectSpotted = true;
            }

            if (objectSpotted)
            {
                _noEffectPlants.Remove(pPlant);
                _affectedPlants.Add(pPlant);
            }
            else
            {
                _noEffectPlants.Add(pPlant);
                _affectedPlants.Remove(pPlant);
            }
        }
        else
        {
            _noEffectPlants.Add(pPlant);
            _affectedPlants.Remove(pPlant);
        }
    }

    private void Update()
    {
        if (transform.childCount <= minimumPlantCount)
        {
            SceneSwitch._instance.scene_changer(3);
        }
    }
}