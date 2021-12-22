using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float lookRadius = 10f;
    public float health = 50f;
    private float speed;
    private Rigidbody rb;

    public GameObject leftFist;

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

    public bool isWeak = false;
    public GameObject explosion;

    public enum StateEnum { Idle, Roam, Chase, Attack, Stagger}
    public StateEnum state;

    // animation things
    Animator animator;
    private string currentState;

    const string DEMON_IDLE = "metarig|Idle";
    const string DEMON_RUN = "metarig|Walk";
    const string DEMON_ATTACK = "metarig|Idle_002";
   
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
        animator = GetComponent<Animator>();
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        CheckState();
        speed = rb.velocity.magnitude;
        
        if (playerInSightRange && !playerInAttackRange && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            state = StateEnum.Chase;
        }
        if (playerInAttackRange && !isWeak)
        {
            state = StateEnum.Attack;
        }
    }
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

        if (health > 0f && health <= 40f)
        {
            state = StateEnum.Stagger;
        }
    }

    void Die ()
    {
        FindObjectOfType<Manager>().Explode(explosion, agent.transform.position);
        Destroy(gameObject);
    }

    void IdleBehaviour()
    {
        leftFist.GetComponent<Collider>().enabled = false;
        Debug.Log("Idle");

        ChangeAnimationState(DEMON_IDLE);
    }

    void ChaseBehaviour()
    {
        leftFist.GetComponent<Collider>().enabled = false;

        agent.SetDestination(target.position);
        ChangeAnimationState(DEMON_RUN);
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
        leftFist.GetComponent<Collider>().enabled = false;

        //change shader

        ChangeAnimationState(DEMON_IDLE);

        isWeak = true;
    }

    void Attack()
    {
        Debug.Log("Attack");

        if (currentState != DEMON_ATTACK)
        {
            ChangeAnimationState(DEMON_ATTACK);
            leftFist.GetComponent<Collider>().enabled = true;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
