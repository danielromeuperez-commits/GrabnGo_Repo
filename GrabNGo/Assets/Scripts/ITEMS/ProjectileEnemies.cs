using UnityEngine;

public class ProjectileEnemies : MonoBehaviour
{
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.DisableMovement(1); //Tiempo inmovil
            }

            Destroy(gameObject);

        }
    }
}
