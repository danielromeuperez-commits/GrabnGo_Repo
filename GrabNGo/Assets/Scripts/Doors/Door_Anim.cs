using UnityEngine;

public class Door_Anim : MonoBehaviour
{
    Animator anim;
    AudioSource Audiosc;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Audiosc = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("character_nearby", true);
            Audiosc.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("character_nearby", false);
            Audiosc.Play();
        }
    }
}
