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
    
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _noEffectPlants.Add(transform.GetChild(i).gameObject);
        }
    }

    public void ChangeGarden()
    {
        int plantsAffected = Random.Range(affectedPlantMinimum, affectedPlantLimit);

        for (int i = 0; i < plantsAffected; i++)
        {
            int affectedPlant = Random.Range(0, transform.childCount-1);
            if (_noEffectPlants.Count != 0)
            {
                while (transform.GetChild(affectedPlant).gameObject != _noEffectPlants[affectedPlant])
                {
                    affectedPlant = Random.Range(0, transform.childCount-1);
                }
            }
            transform.GetChild(affectedPlant).GetComponent<PlantHealthScript>().StartDamagingPlant();
            _noEffectPlants.Remove(transform.GetChild(affectedPlant).gameObject);
            _affectedPlants.Add(transform.GetChild(affectedPlant).gameObject);
        }

        foreach (GameObject affectedPlant in _affectedPlants)
        {
            affectedPlant.GetComponent<PlantHealthScript>().DamagePlant();
        }
    }
}//