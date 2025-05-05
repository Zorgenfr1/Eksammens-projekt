using UnityEngine;

public class ZoneDistanceTrigger : MonoBehaviour
{
    public Transform player;
    public WolvenAI wolfAI;
    public float triggerDistance = 20f;

    private bool hasTriggered = false;

    void Update()
    {
            float distanceFromCenter = Vector3.Distance(player.position, Vector3.zero);

            if (distanceFromCenter > triggerDistance)
            {
                Debug.Log("Spilleren har forladt midten af mappet. Ulven jager!");
                wolfAI.StartHowl();
                hasTriggered = true;
            }
            else if (distanceFromCenter <= triggerDistance) 
            {
                wolfAI.StopChase();
            }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, triggerDistance);
    }
}
