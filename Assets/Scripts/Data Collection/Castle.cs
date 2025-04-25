using UnityEngine;

public class Castle : MonoBehaviour
{
    public bool insideCastle = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideCastle = true;
        }
    }
}
