using UnityEngine;

public class SneakAttack : MonoBehaviour
{
    private RaycastHit hit;

    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f))
        {
            if (hit.transform.gameObject.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject);
            }
        }
    }
}
