using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class MonsterAI : MonoBehaviour

{
    private NavMeshAgent agent;
    private Transform player;
    private Vector3 lastKnownPlayerPosition;
    private int currentPatrolIndex = 0;
    [SerializeField] private Transform[] waypoints;
    public float viewDistance = 5f;
    public float viewAngle = 55f;
    private Vector3 target;
    public float patrolSpeed = 0.5f;
    public float chaseSpeed = 1.0f;
    //Animator animator;
    //GameObject canvas;
    public Transform head;


    private enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }

    private EnemyState currentState;
    [SerializeField] private float killRange;
    [SerializeField] private LayerMask playerLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = EnemyState.Patrol;
        SetDestination(waypoints[currentPatrolIndex].position);
        //animator = GetComponent<Animator>();
        //canvas = GameObject.FindGameObjectWithTag("Canvas");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                CheckForPlayer();
                Patrol();
                break;
            case EnemyState.Chase:
                CheckForPlayer();
                Chase();
                break;
            case EnemyState.Attack:
                KillPlayer();
                break;
            default:
                break;
        }
        SetDestination(target);
        //animator.SetFloat("Speed", agent.speed);
    }
    void Patrol()
    {
        agent.speed = patrolSpeed;
        if (agent.remainingDistance < 0.5f)
        {
            target = waypoints[currentPatrolIndex].position;
            currentPatrolIndex = (currentPatrolIndex + 1) % waypoints.Length;
        }
    }

    void Chase()
    {
        agent.speed = chaseSpeed;
        target = lastKnownPlayerPosition;
        RaycastHit hit;
        if (Physics.Raycast(head.position, player.transform.position - transform.position, out hit, viewDistance))
        {
            if (hit.collider.tag == "Player" && Vector3.Distance(transform.position, player.position) < killRange)
            {
                currentState = EnemyState.Attack;
            }
        }

        if (agent.remainingDistance < 0.5f)
        {
            target = waypoints[currentPatrolIndex].position;
            currentState = EnemyState.Patrol;
        }
    }

    void KillPlayer()
    {
        Debug.Log("Attacking Player");
    }

    void CheckForPlayer()
    {
        RaycastHit hit;
        float DotProduct;
        Vector3 vectorToPlayer;
        vectorToPlayer = (player.position - transform.position);
        DotProduct = Vector3.Dot(vectorToPlayer.normalized, transform.forward);
        if (Physics.Raycast(head.position, player.transform.position - transform.position, out hit, viewDistance))
        {
            if (hit.collider.tag == "Player" && DotProduct >= Mathf.Cos(viewAngle))
            {
                currentState = EnemyState.Chase;
                lastKnownPlayerPosition = player.position;
            }

        }
        Debug.DrawLine(head.position, player.position);
    }
    void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void LoseSightOfPlayer()
    {
        SetDestination(lastKnownPlayerPosition);

    }

    public void ReturnToPatrol()
    {
        currentState = EnemyState.Patrol;
        SetDestination(waypoints[currentPatrolIndex].position);
    }
}