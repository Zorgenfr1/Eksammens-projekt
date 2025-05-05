using UnityEngine;

public class SneakAttack : MonoBehaviour
{
    private RaycastHit hit;

    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            if (hit.transform.gameObject.CompareTag("Enemy") && /*hit.transform.forward == transform.forward &&*/ Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Enemy Sneak Attacked");
                EnemyHealth enemy = hit.transform.gameObject.GetComponent<EnemyHealth>();
                enemy.ChangeHealth(-100);
            }
        }
    }
}
