using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    private PlayerManager playerManager;
    Transform player;
    

    bool hasInteracted = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    public virtual void Interact()
    {
        Debug.Log("Interacting with" + transform.name);
    }
    private void Update()
    {
    
        float distance = Vector3.Distance(player.position, interactionTransform.position);
        if (distance < radius && !hasInteracted)
        {
            Interact();
            hasInteracted = true;
        }
        if (distance > radius)
        {
            hasInteracted = false;
        }
    
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
