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

    AudioSource AudioSource;

    private void Awake()
    {
        anim= objectToAnimate.GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerPoints.ActualPoints >= requiredPoints)
            {
                anim.SetBool("character_nearby", true);
                AudioSource.Play();

            }
            else
            {
                warningText.SetActive(true);
                Invoke(nameof(HideText), 2f);
            }
        }
    }

    void HideText()
    {
        warningText.SetActive(false);
    }
}