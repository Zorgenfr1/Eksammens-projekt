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
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, 7);
        foreach (Collider collider in enemyColliders)
        {
            if (CompareTag("Player"))
            {
                Debug.Log("Coin is in Guard's radius");
                MonsterAI enemy = collider.GetComponent<MonsterAI>();
                enemy.target = transform.position;
            }
        }
    }
}
