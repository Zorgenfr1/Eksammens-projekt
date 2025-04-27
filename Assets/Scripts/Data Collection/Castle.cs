using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private Tresure player;
    public bool insideCastle = false;
    public bool escapedCastle = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideCastle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && player.hasTresure == true)
        {
            escapedCastle = true;
        }
    }
}
