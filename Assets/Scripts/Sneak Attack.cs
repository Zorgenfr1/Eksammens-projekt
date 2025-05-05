using UnityEngine;

public class SneakAttack : MonoBehaviour
{
    private RaycastHit hit;
    public Animator arms;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            if (hit.transform.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.E))
            {
                Vector3 enemyForward = hit.transform.forward;
                Vector3 toPlayer = (transform.position - hit.transform.position).normalized;
                float angle = Vector3.Angle(enemyForward, toPlayer);

                if (angle > 70f)
                {
                    arms.SetTrigger("Stab");
                    EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                    enemy.ChangeHealth(-100);
                }
            }
        }
    }
}

