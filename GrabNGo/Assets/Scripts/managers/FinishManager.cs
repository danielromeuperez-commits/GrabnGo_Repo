using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    public Timer timer;
    public string victorySceneName = "VictoryScene";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.StopTimer();
            float tiempoFinal = timer.CurrentTime;

            // Agrega el intento a la lista
            GameManager.Instance.AddScore(GameManager.Instance.currentPlayerName, tiempoFinal);

            SceneManager.LoadScene(victorySceneName);
        }
    }
}