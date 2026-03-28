using UnityEngine;

public class ProjectileEnemies : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null )
            {
                player.DisableMovement(1); //Tiempo inmovil
            }
            
            Destroy(gameObject);

        }
    }
}
