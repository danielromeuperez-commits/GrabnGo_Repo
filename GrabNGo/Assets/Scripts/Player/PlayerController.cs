using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region General Variables
    [Header("Movimiento & Mirar")]
    [SerializeField] GameObject camHolder; //ref al obj q tiene como hijo la cámara (rota por la cámara)
    [SerializeField] float speed = 5;
    [SerializeField] float sprintSpeed = 8;
    [SerializeField] float crounchSpeed = 3;
    [SerializeField] float maxForce = 1; //Fuerza max de aceleración
    [SerializeField] float sensitivity = 0.1f; //Sensibilidad para input
    [SerializeField] int blockSecs = 1; //Segundos en los que el input será bloqueado

    [Header("Jump & Groundcheck")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundcheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;

    [Header("Player State Bools")]
    [SerializeField] bool isSprinting;
    [SerializeField] bool isCrouching;
    [SerializeField] bool inputEnabled;
    #endregion

    [SerializeField] int totalPoints;

    //Variables de ref privadas:
    Rigidbody rb; //ref al rb del PL
    Animator anim; //ref añ animator del PL

    //Variables para input:
    Vector2 MoveInput;
    Vector2 lookInput;
    float lookRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //lock cursor ratón
        Cursor.lockState = CursorLockMode.Locked; //Mueve curson a centro
        Cursor.visible = false; //Oculta cursor
        inputEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, groundcheckRadius, groundLayer);
        //dibujar un rayo ficticio en escena para orientacion de la camara
        Debug.DrawRay(camHolder.transform.position, camHolder.transform.forward * 100f, Color.red);

    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void CameraLook()
    {
        if(!inputEnabled) return;
        //rotacion horizontal del cuerpo del PJ
        transform.Rotate(Vector3.up * lookInput.x * sensitivity);
        //Rotacion vertical (la camara la lleva)
        lookRotation += (-lookInput.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.localEulerAngles = new Vector3(lookRotation, 0f, 0f);
    }

    void Movement()
    {
        if (!inputEnabled)
        {
            //parar movimiento mientras está bloqueado
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        Vector3 currentVelocity = rb.linearVelocity; //Ncesitamos calcular la velocidad del RB constantemente
        Vector3 targetVelocity = new Vector3(MoveInput.x, 0, MoveInput.y); //Vel a alcanzar = la direccion que pulses
        targetVelocity *= isCrouching ? crounchSpeed : (isSprinting ? sprintSpeed : speed);

        //Convertir direccion local en global
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculo de velocidad, ACELERACION
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0f, velocityChange.z);
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);

        //Aplicar fuerza de movimiento / aceleracion
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void jump()
    {
        if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void DisableMovement(float time)
    {
        StartCoroutine(DisableMovementRoutine(time));
    }

    IEnumerator DisableMovementRoutine(float time)
    {
        inputEnabled = false;
        MoveInput = Vector2.zero;
        
        rb.linearVelocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(time);

        inputEnabled = true;
    }

    public int TotalPoints
    {
        get { return totalPoints; }
    }

    public void AddPoints(int amount)
    {
        totalPoints += amount;
        Debug.Log("Puntos: " + totalPoints);
    }

    #region INPUT METHODS
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;
        MoveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;
        if (context.performed) jump();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;
        if (context.performed)
        {
            isCrouching = !isCrouching;
            anim.SetBool("isCrouching", isCrouching);
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;
        if (context.performed && !isCrouching) isSprinting = true;
        if (context.canceled) isSprinting = false;
    }
    #endregion
}
