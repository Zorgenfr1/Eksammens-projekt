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
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 3, layerMask) && Input.GetKeyDown(KeyCode.E))
            {
                hasTresure = true;

                tresure.SetActive(false);
            }
        }
    }
}
