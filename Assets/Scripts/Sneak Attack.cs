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
                Health enemy = hit.transform.GetComponent<Health>();
                enemy.ChangeHealth(100);
            }
        }
    }
}
