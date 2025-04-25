using Unity.VisualScripting;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public Attacks lightAttack;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Health player = other.GetComponent<Health>();
            player.ChangeHealth(-lightAttack.damage);
        }
    }
}
