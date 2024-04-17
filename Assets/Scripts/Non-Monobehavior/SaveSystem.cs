using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveScore(string pDifficulty, int pScore)
    {
        ScoreData scoreData = LoadScore();
        if (scoreData != null)
        {
            bool difficultySaved = false;
            for (int i = 0; i < scoreData.difficultyData.Count; i++)
            {
                if (scoreData.difficultyData[i].Item1 == pDifficulty && scoreData.difficultyData[i].Item2 < pScore)
                {
                    scoreData.difficultyData[i] = (scoreData.difficultyData[i].Item1, pScore);
                    difficultySaved = true;
                }
            }
            if (!difficultySaved)
                scoreData.difficultyData.Add((pDifficulty, pScore));
        }
        else
            scoreData = new ScoreData(pDifficulty, pScore);
        
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = $"{ Application.persistentDataPath}/score.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream,scoreData);
        stream.Close();
    }

    public static ScoreData LoadScore()
    {
        string path = $"{ Application.persistentDataPath}/score.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            
            return data;
        }
        
        return null;
    }
}
