using UnityEngine;

public class Tresure : MonoBehaviour
{
    [SerializeField] private GameObject tresure;
    [SerializeField] private LayerMask layerMask;
    public bool hasTresure = false;

    void Update()
    {
        if(Vector3.Distance(tresure.transform.position, transform.position) < 7)
        {
            Debug.Log("Close to tresure");
                Debug.DrawLine(transform.position, tresure.transform.position, Color.red);
            if(Physics.Raycast(transform.position + Vector3.up, transform.forward, 3, layerMask) && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("raycasting to tresure");
                hasTresure = true;

                tresure.SetActive(false);
            }
        }
    }
}
