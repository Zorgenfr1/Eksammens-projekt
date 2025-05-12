using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public float coinDetectionRange;
    public Vector3 distanceToEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, coinDetectionRange);
        foreach (Collider collider in enemyColliders)
        {

            if (collider.CompareTag("Enemy"))
            {
                EnemyAI enemy = collider.GetComponent<EnemyAI>();
                enemy.target = transform.position;
                distanceToEnemy = enemy.transform.position - transform.position;
                if (distanceToEnemy.magnitude > 0.6f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
