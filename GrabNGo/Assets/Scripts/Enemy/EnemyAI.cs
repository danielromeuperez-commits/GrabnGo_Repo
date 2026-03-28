using UnityEngine;
using UnityEngine.AI; //Libreria de comp. NavMesh

public class EnemyAI : MonoBehaviour
{
    #region General Variables
    [Header("AI config")]
    [SerializeField] NavMeshAgent agent; //ref al cerebro del agente
    [SerializeField] Transform target; // ref al target
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask groundLayer;
    [Header("Patroling stats")]
    [SerializeField] float walkpointRange = 10f; //radio max para determinar puntos a perseguir
    Vector3 walkPoint; //posición del punto random a perseguir
    bool walkPointSet; //Hay punto a perseguir generado? si es faslse genera uno

    [Header("Patroling stats")]
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] GameObject projectile; //ref a la bala q dispara el enemigo
    [SerializeField] Transform shootPoint; //posicion desde la q se dispara la bala
    [SerializeField] float shootSpeedY; //Fuerza de disparo arriba (catapulta)
    [SerializeField] float shootSpeedZ; //Fuerza de disparo hacia alante
    bool alreadyAttacked;

    [Header("States & detection")]
    [SerializeField] float sightRange = 8f; //Radio del detector de persecución
    [SerializeField] float attackRange = 2f; //Radio del detector de ataque
    [SerializeField] bool targetInSightRange; //Determina si es verdadero q podemos perseguir al target
    [SerializeField] bool targetInAttackRange; //Determian si es verdadero q ataquemos al target

    [Header("stuck Detection")]
    [SerializeField] float stuckCheckTime = 2f; //tiempo q el agente espera estando quieto antes de darse cuenta q está stuck
    [SerializeField] float stuckThreshold = 0.1f; //margen de detección de stuck
    [SerializeField] float maxStuckDuration = 3f; //Tiempo max de estar stuck

    float stuckTimer; //reloj q cuenta el tiempo de estar stuck
    float lastCheckTime; //Tiempo de chequeo previo de stuck
    Vector3 lastPosition; //posición del ultimo walkpoint
    #endregion

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastPosition = transform.position;
        lastCheckTime = Time.time;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyStateUpdate();
    }

    void EnemyStateUpdate()
    {
        //Metodo que se encarga de gestionar el cambio de estados en el enemigo

        //1- cambio de estado de los booleanos 
        //Detectamos si los targets están en visión
        Collider[] hits = Physics.OverlapSphere(transform.position, sightRange, targetLayer);
        targetInSightRange = hits.Length > 0;
        //Si estan en visión, detectamos si ataca
        if (targetInSightRange)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            targetInAttackRange = distance <= attackRange;
        }
        else
        {
            targetInAttackRange = false;
        }

        //2- cambio de estados segun booleanos
        if (!targetInSightRange && !targetInAttackRange)
        {
            Patrolling();
        }
        else if (targetInSightRange && !targetInAttackRange)
        {
            ChaseTarget();
        }
        else if (targetInSightRange && targetInAttackRange)
        {
            AttackTarget();
        }
    }

    void Patrolling()
    {
        Debug.Log("ENEMIGO PATRULLANDO");
    }
    void ChaseTarget()
    {
        //Acción que le dice al agente que persiga al target
        agent.SetDestination(target.position);
    }
    void AttackTarget()
    {
        //Accion que contiene la logica de ataque
        //1- Hacer que el agente se quede quieto (Se persigue a si mismo)
        agent.SetDestination(transform.position);
        //2- Aplicar rotación suavizada para que el agente mire al target antes de atacar
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, agent.angularSpeed * Time.deltaTime);
        }
        //3- Se ataca (solo si no se está atacando)
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shootSpeedZ, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) return; //Si jugamos en BUILD no se ejecuta el resto del codigo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
