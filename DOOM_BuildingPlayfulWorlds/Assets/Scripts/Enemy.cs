using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float lookRadius = 10f;
    public float health = 50f;
    private float speed;
    private Rigidbody rb;

    public Transform target;
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    public enum StateEnum { Idle, Roam, Chase, Attack, Stagger}
    public StateEnum state;
   
    private void CheckState()
    {
        switch (state)
        {
            case StateEnum.Idle: IdleBehaviour(); break;
            case StateEnum.Chase: ChaseBehaviour(); break;
            case StateEnum.Attack: AttackBehaviour(); break;
            case StateEnum.Stagger: StaggerBehaviour(); break;
        }
    }

    private void Awake()
    {
        target = GameObject.Find("FirstPersonCharacter").transform;
    }
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        CheckState();
        speed = rb.velocity.magnitude;
        
        if (playerInSightRange && !playerInAttackRange)
        {
            state = StateEnum.Chase;
        }
        if (playerInAttackRange)
        {
            state |= StateEnum.Attack;
        }
    }
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }

    void IdleBehaviour()
    {
        Debug.Log("Idle");
    }

    void ChaseBehaviour()
    {
        agent.SetDestination(target.position);
    }

    void AttackBehaviour()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            Attack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }          
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void StaggerBehaviour()
    {

    }

    void Attack()
    {
        Debug.Log("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}