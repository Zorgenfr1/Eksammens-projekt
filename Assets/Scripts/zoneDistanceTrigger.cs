using UnityEngine;

public class ZoneDistanceTrigger : MonoBehaviour
{
    public Transform player;
    public WolvenAI wolfAI;
    public float triggerDistance = 20f;

    void Update()
    {
            float distanceFromCenter = Vector3.Distance(player.position, Vector3.zero);

            if (distanceFromCenter > triggerDistance)
            {
                wolfAI.StartHowl();
            }
            else if (distanceFromCenter <= triggerDistance) 
            {
                wolfAI.StopChase();
            }
    }
    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, triggerDistance);
    }*/
}
