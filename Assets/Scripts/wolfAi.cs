using UnityEngine;
using UnityEngine.AI;

public class WolvenAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public Animator wolfAni;
    public float attackRange = 10f;
    public float damage = 1000f;

    private bool shouldChase = false;

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
        Debug.Log("Ulven angriber spilleren!");
        StopChase();
        wolfAni.SetTrigger("Attack");
    }

    public void StartHowl()
    {
        wolfAni.SetTrigger("OutsideZone");
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
