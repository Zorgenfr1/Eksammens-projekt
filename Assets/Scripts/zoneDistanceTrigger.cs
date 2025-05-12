using System.Collections.Generic;
using UnityEngine;

public class ZoneDistanceTrigger : MonoBehaviour
{
    public Transform player;
    public List<WolvenAI> wolves = new List<WolvenAI>();
    public float triggerDistance = 20f;

    void Update()
    {
        float distanceFromCenter = Vector3.Distance(player.position, Vector3.zero);

        foreach (WolvenAI wolf in wolves)
        {
            if (distanceFromCenter > triggerDistance)
            {
                wolf.StartHowl();
            }
            else
            {
                wolf.StopChase();
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, triggerDistance);
    }
}
