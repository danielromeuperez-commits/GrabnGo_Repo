using UnityEngine;

public class PointRequirement : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] int requiredPoints;

    [Header("References")]
    [SerializeField] GameObject objectToDisable;
    [SerializeField] Collectable playerPoints;

    [Header("UI")]
    [SerializeField] GameObject warningText;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerPoints.ActualPoints >= requiredPoints)
            {
                objectToDisable.SetActive(false);
            }
            else
            {
                warningText.SetActive(true);
                Invoke(nameof(HideText), 2f); // desaparece en 2 segundos
            }
        }
    }

    void HideText()
    {
        warningText.SetActive(false);
    }
}