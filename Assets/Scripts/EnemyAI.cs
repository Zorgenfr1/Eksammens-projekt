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
    public Animator animator;
    //GameObject canvas;
    public Transform head;
    public Vector3 towardsPlayer;
    public bool playerCrouching = true;
    public bool playerSeen;
    public Vector3 playerHiddenLocation;

    public AudioClip detectionAudio;
    AudioSource audio;
    private bool hasPlayedDetectionSound = false;
    public AudioClip loseSightAudio;
    private bool hasPlayedLoseSightSound = false;
    public AudioClip investigateAudio;
    private bool hasPlayedInvestigateSound = false;

    [Header("Attacks")]
    public Attacks lightAttack;
    public Attacks heavyAttack;


    private enum EnemyState
    {
        Patrol,
        Chase,
        Combat,
        Investigate
    }

    private EnemyState currentState;
    [SerializeField] private float killRange;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = EnemyState.Patrol;
        SetDestination(waypoints[currentPatrolIndex].position);
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
            case EnemyState.Combat:
                CheckForPlayer();
                Combat();
                break;
            case EnemyState.Investigate:
                CheckForPlayer();
                Investigate();
                break;
            default:
                break;
        }
        SetDestination(target);
        animator.SetFloat("Speed", agent.speed, 0.5f, Time.deltaTime);
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
        hasPlayedLoseSightSound = false;
        agent.speed = chaseSpeed;
        target = lastKnownPlayerPosition;
        RaycastHit hit;
        if (Physics.Raycast(head.position, player.transform.position - transform.position, out hit, viewDistance))
        {
            if (hit.collider.tag == "Player" && Vector3.Distance(transform.position, player.position) < killRange)
            {
                currentState = EnemyState.Combat;
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
           // if (!hasPlayedLoseSightSound)
           // {
            //    PlayAudio(loseSightAudio, ref hasPlayedLoseSightSound);
            //}
        }
       /* if (agent.remainingDistance < 0.5f && !playerCrouching)
        {
            Attack();
        } */
    }

    void Combat()
    {
        target = lastKnownPlayerPosition;
        agent.speed = chaseSpeed;
        Debug.Log("Combat state");
        RaycastHit hit;
        if (!Physics.Raycast(head.position, player.transform.position - transform.position, out hit, viewDistance) && playerSeen)
        {
            StartCoroutine(assumePosition());
        }
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            Attack();
        }

    }

    void Attack()
    {
        animator.runtimeAnimatorController = lightAttack.AOC;
        animator.Play("CombatLayer.Combat", 0, 0);
        Debug.Log("Attacking");
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
                
                if (!hasPlayedDetectionSound)
                {
                    PlayAudio(detectionAudio, ref hasPlayedDetectionSound);
                }
            }

        }
        else
        {
            hasPlayedDetectionSound = false;
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


        if (!hasPlayedInvestigateSound)
        {
            PlayAudio(investigateAudio, ref hasPlayedInvestigateSound);
        }

        if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(lastKnownPlayerRotation)) < 1f)
        {
            target = playerHiddenLocation;

            agent.speed = patrolSpeed;
            
            if (agent.remainingDistance < 0.5f)
            {
                StartCoroutine(lookingAround()); ;
            }
        }
    }


    IEnumerator assumePosition()
    {
        yield return new WaitForSeconds(1f);
        lastKnownPlayerRotation = player.position - transform.position;
        playerHiddenLocation = player.position;
        playerSeen = false;
        currentState = EnemyState.Chase;
        yield break;
    }

    IEnumerator lookingAround()
    {
        yield return new WaitForSeconds(2f);
        currentState = EnemyState.Investigate;
        yield break;

    }

    public void PlayAudio(AudioClip clip, ref bool hasPlayed)
    {
        audio.PlayOneShot(clip);
        hasPlayed = true;
    }


    void Investigate()
    {
        hasPlayedInvestigateSound = true;
        target = playerHiddenLocation;
        agent.speed = patrolSpeed;
        if (agent.remainingDistance < 0.5f)
        {
            PlayAudio(loseSightAudio, ref hasPlayedLoseSightSound);
            currentState = EnemyState.Patrol;
        }

    }

    public void ReturnToPatrol()
    {
        currentState = EnemyState.Patrol;
        SetDestination(waypoints[currentPatrolIndex].position);
    }
}