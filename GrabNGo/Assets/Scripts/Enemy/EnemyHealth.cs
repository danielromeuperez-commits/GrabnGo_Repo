using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health system config")]
    [SerializeField] int health; //Vida actual
    [SerializeField] int maxHealth; //Vida maxima

    [Header("Feedback config")]
    [SerializeField] Material damagedMat; //Ref añ material q da feedback de dañado
    [SerializeField] MeshRenderer enemyRend; //Ref añ remderer del modelo del enemigo
    [SerializeField] GameObject deathVFX;
    Material baseMat; //Ref al mat base del modelo del enemigo
    private void Awake()
    {
        health = maxHealth; //Cuando se genera el enemigo, su vida actual se carga a la maxima
        baseMat = enemyRend.material; //Se almacena el material base del enemigo
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            health = 0; //La vida no puede bajad de 0
            deathVFX.SetActive(true); //Encender el VFX
            deathVFX.transform.position = transform.position; //colocarlo en la posicion del enemigo
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; //quitar vida como valor de damage
        enemyRend.material = damagedMat; //Se cambia el mat base al mat dañado
        Invoke(nameof(ResetEnemyMat), 0.1f); //Llamar al reseteo de material con 0.1 de espera
    }
    void ResetEnemyMat()
    {
        enemyRend.material = baseMat; //Cambiar el mat del mdoelo al base
    }
}
