using UnityEngine;

public class PointRequirement : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] int requiredPoints;

    [Header("References")]
    [SerializeField] GameObject objectToAnimate;
    [SerializeField] Collectable playerPoints;
    [SerializeField] Animator anim;

    [Header("UI")]
    [SerializeField] GameObject warningText;

    private void Awake()
    {
        anim= objectToAnimate.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerPoints.ActualPoints >= requiredPoints)
            {
                anim.SetBool("character_nearby", true);
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