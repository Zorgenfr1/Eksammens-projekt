using UnityEngine;
using UnityEngine.AI;

public class WolvenAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float attackRange = 2f;
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
        // Her skal du evt. kalde spillerens "TakeDamage" funktion
        Debug.Log("Ulven angriber spilleren!");
        // Du kan f.eks. deaktivere jagten efter angreb:
        shouldChase = false;
    }

    public void StartChase()
    {
        shouldChase = true;
    }
}
