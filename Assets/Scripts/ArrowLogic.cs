using System.Collections;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    private GameObject arrow;
    public Attacks arrowAttack;
    private void Start()
    {
        arrow = GetComponent<GameObject>();
        StartCoroutine(ArrowDespawn());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.LogError("Arrow hit");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.ChangeHealth(-arrowAttack.damage);
        }
    }

    IEnumerator ArrowDespawn()
    {
        yield return new WaitForSeconds(10f);
        Destroy(arrow);
    }
}
