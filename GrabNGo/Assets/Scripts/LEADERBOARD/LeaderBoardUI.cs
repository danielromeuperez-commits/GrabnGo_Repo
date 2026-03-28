using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public TMP_Text leaderboardText; // 1 solo TMP_Text con salto de línea

    void OnEnable()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        string text = "";

        for (int i = 0; i < GameManager.Instance.leaderboard.Count && i < 10; i++)
        {
            var score = GameManager.Instance.leaderboard[i];
            text += $"{i + 1}. {score.playerName} - {score.time:F3}s\n";
        }

        leaderboardText.text = text;
    }
}