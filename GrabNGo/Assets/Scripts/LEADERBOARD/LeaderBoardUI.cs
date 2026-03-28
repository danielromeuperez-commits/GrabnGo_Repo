using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public TMP_Text leaderboardText; // 1 TMP_Text con salto de línea

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

            // Formatear tiempo igual que el Timer
            int minutes = Mathf.FloorToInt(score.time / 60f);
            int seconds = Mathf.FloorToInt(score.time % 60f);
            int milliseconds = Mathf.FloorToInt((score.time * 1000f) % 1000f);

            string formattedTime = $"{minutes:00}:{seconds:00}:{milliseconds:000}";

            text += $"{i + 1}. {score.playerName} - {formattedTime}\n";
        }

        leaderboardText.text = text;
    }
}