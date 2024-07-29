using Assets.Scripts.Models;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HighScore _highScore = new () 
    { 
        Name = string.Empty,
        Score = 0 
    };


    public string HighScoreName => _highScore.Name;
    public int HighScore => _highScore.Score;

    public string CurrentName;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    private string GetSaveFilePath => Application.persistentDataPath + "/saveFile.json";

    public void LoadHighScore()
    {
        if (File.Exists(GetSaveFilePath))
        {
            var json = File.ReadAllText(GetSaveFilePath);
            var data = JsonUtility.FromJson<HighScore>(json);

            _highScore.Score = data == null ? default : data.Score;
            _highScore.Name = data == null ? default : data.Name;
        }
        else
        {
            _highScore.Score = 0;
            _highScore.Name = string.Empty;
        }
    }

    private void SaveHighScore()
    {
        var data = new HighScore()
        {
            Score = _highScore.Score,
            Name = _highScore.Name
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(GetSaveFilePath, json);
    }

    public void SetNewHighScore(int score)
    {
        if (score <= _highScore.Score)
            return;
        _highScore.Name = CurrentName;
        _highScore.Score = score;
        SaveHighScore();
    }
}
