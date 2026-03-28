using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<PlayerScore> leaderboard = new List<PlayerScore>();
    public string currentPlayerName; // guardamos el nombre actual

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(string playerName, float time)
    {
        leaderboard.Add(new PlayerScore { playerName = playerName, time = time });
        leaderboard.Sort((a, b) => a.time.CompareTo(b.time));
        if (leaderboard.Count > 10)
            leaderboard.RemoveAt(leaderboard.Count - 1); // top 10
    }
}

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public float time;
}