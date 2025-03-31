using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class MonsterAI : MonoBehaviour

{
    private NavMeshAgent agent;
    private Transform player;
    private Vector3 lastKnownPlayerPosition;
    private Vector3 lastKnownPlayerRotation;
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
    public Vector3 towardsPlayer;
    public bool playerCrouching = true;
    public bool playerSeen;
    public Vector3 playerHiddenLocation;


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
        if (!Physics.Raycast(head.position, player.transform.position - transform.position, out hit, viewDistance) && playerSeen)
        {
            StartCoroutine(assumePosition());
        }
        towardsPlayer = player.position - transform.position;
        
        if (agent.remainingDistance < 0.5f && playerCrouching)
        {
            LookAround();
        }
        if (agent.remainingDistance < 0.5f && !playerCrouching)
        {
            KillPlayer();
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
                playerSeen = true;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 5f);
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

    void LookAround()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lastKnownPlayerRotation), 5f);
        Debug.Log("ahh");
        if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(lastKnownPlayerRotation)) < 1f)
        {
            target = playerHiddenLocation;
            
            if (agent.remainingDistance < 0.5f)
            {
                StartCoroutine(lookingAround()); ;
            }
        }
    }


    IEnumerator assumePosition()
    {
        Debug.Log("Starting timer"); ;
        yield return new WaitForSeconds(1f);
        lastKnownPlayerRotation = player.position - transform.position;
        playerHiddenLocation = player.position;
        Debug.Log("Timer sluttet efter 1 sekund");
        playerSeen = false;
        currentState = EnemyState.Chase;
        yield break;
    }

    IEnumerator lookingAround()
    {
        Debug.Log("Looking Around");
        yield return new WaitForSeconds(2f);
        Debug.Log("Stopped looking around");
        currentState = EnemyState.Patrol;
        yield break;
    }

    public void LoseSightOfPlayer()
    {
       

    }

    public void ReturnToPatrol()
    {
        currentState = EnemyState.Patrol;
        SetDestination(waypoints[currentPatrolIndex].position);
    }
}