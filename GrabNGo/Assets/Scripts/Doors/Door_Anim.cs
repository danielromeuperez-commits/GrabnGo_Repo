using UnityEngine;

public class Door_Anim : MonoBehaviour
{
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("character_nearby", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("character_nearby", false);
        }
    }
}
