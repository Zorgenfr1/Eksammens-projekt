using UnityEngine;

public class OuterWall : MonoBehaviour
{
    public bool insideTheOuterWall = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            insideTheOuterWall = true;
        }
    }
}
