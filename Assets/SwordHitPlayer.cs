using UnityEngine;

public class SwordHitPlayer : MonoBehaviour
{
    public Attacks lightAttack;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            Health enemy = other.GetComponent<Health>();
            enemy.ChangeHealth(-lightAttack.damage);
        }
    }
}
