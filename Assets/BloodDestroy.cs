using UnityEngine;

public class BloodDestroy : MonoBehaviour
{
    private void Start()
    {
        Destroy();
        Debug.LogError("Blood spawned");
    }

    void Destroy()
    {
        Destroy(gameObject, 5f);
    }

}
