using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Necesario para trabajar con el New Input System

public class GunSystem : MonoBehaviour
{
    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam; //Referencia a la cámara desde cuyo centro se dispara (Raycast desde centro cámara)
    [SerializeField] RaycastHit hit; //Referencia a la info de impacto de los disparos (información de impacto Raycast)
    [SerializeField] LayerMask interactableLayer; //Referencia a la Layer que puede impactar el disparo
    [SerializeField] AudioSource weaponSound; //Referencia al AudioSource del arma

    [Header("Interactable Stats")]
    public float range; //Alcance de disparo (longitud del Raycast)
    public float shootingCooldown; //Tiempo de enfriamiento del arma
    public int damage; //Daño del arma

    [Header("State Bools")]
    [SerializeField] bool shooting; //Verdadero cuando estamos disparando
    [SerializeField] bool canShoot; //Verdadero cuando podemos disparar

    [Header("Feedback & Graphics")]
    [SerializeField] GameObject muzzleFlash; //Objeto feedback del fogonazo
    [SerializeField] bool attackIsSounding; //Si es verdadero, el sonido de disparo ya suena

    #endregion

    private void Awake()
    {
        weaponSound = GetComponent<AudioSource>();
        attackIsSounding = false;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        //Lectura constante del Raycast si se reúnen las condiciones
        if (canShoot && shooting)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        canShoot = false; //Estamos en el proceso de disparo

        Vector3 direction = fpsCam.transform.forward;

        //Raycast del disparo
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, interactableLayer))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyScript = hit.collider.GetComponent<EnemyHealth>();
                enemyScript.TakeDamage(damage);
            }
        }

        if (!IsInvoking(nameof(ResetShoot)) && !canShoot)
        {
            Invoke(nameof(ResetShoot), shootingCooldown);
        }
    }

    void ResetShoot()
    {
        canShoot = true;
    }

    #region Input System
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            shooting = true;
        }
        else if (context.canceled)
        {
            shooting = false;
        }
    }
    #endregion
}