using UnityEngine;

public class OuterWall : MonoBehaviour
{
    public bool insideTheOuterWall = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideTheOuterWall = true;
        }
    }
}
