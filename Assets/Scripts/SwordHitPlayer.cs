using UnityEngine;

public class SwordHitPlayer : MonoBehaviour
{
    public Attacks lightAttack;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.LogError("Sword hit enemy");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.ChangeHealth(-lightAttack.damage);
        }
    }
}
    