using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WolvenAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public Animator wolfAni;
    public float attackRange = 10f;
    public float damage = 1000f;
    public AudioClip howlSound;
    private AudioSource wolfSound;

    private bool shouldChase = false;


    private void Start()
    {
        wolfSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (shouldChase && player != null)
        {
            agent.SetDestination(player.position);

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        //StopChase();
        wolfAni.SetTrigger("Attack");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            health.Died.Invoke();
        }
    }

    public void StartHowl()
    {
        wolfAni.SetTrigger("OutsideZone");
    }

    public void HowlSound()
    {
        wolfSound.PlayOneShot(howlSound);
    }

    public void StartChase()
    {
        shouldChase = true;
        agent.isStopped = false;
    }

    public void StopChase()
    {
        shouldChase = false;
        agent.isStopped = true;
    }
}
