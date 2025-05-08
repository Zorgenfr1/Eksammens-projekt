using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, 40);
        foreach (Collider collider in enemyColliders)
        {

            if (collider.CompareTag("Player"))
            {
                Debug.Log("Coin is in Guard's radius");
                EnemyAI enemy = collider.GetComponent<EnemyAI>();
                enemy.target = transform.position;
            }
        }
    }
}
