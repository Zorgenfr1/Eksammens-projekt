using UnityEngine;

public class Castle : MonoBehaviour
{
    public bool insideCastle = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            insideCastle = true;
        }
    }
}
