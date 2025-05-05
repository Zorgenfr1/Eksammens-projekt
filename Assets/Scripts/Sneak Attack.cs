using UnityEngine;

public class SneakAttack : MonoBehaviour
{
    private RaycastHit hit;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            if (hit.transform.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.E))
            {
                Vector3 enemyForward = hit.transform.forward;
                Vector3 toPlayer = (transform.position - hit.transform.position).normalized;
                float angle = Vector3.Angle(enemyForward, toPlayer);

                if (angle > 90f)
                {
                    EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                    enemy.ChangeHealth(-100);
                }
            }
        }
    }
}

