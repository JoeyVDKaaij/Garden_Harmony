using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public List<(string, int)> difficultyData = new List<(string, int)>();
    public ScoreData(string pDifficulty, int data)
    {
        difficultyData.Add((pDifficulty, data));
    }
}
