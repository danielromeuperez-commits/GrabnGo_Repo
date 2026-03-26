using UnityEngine;

public class ColliderTimer : MonoBehaviour
{
    public Timer timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.StartTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.StopTimer();
        }
    }
}
