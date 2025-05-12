using UnityEngine;

public class BloodDestroy : MonoBehaviour
{
    private void Start()
    {
        Destroy();
    }

    void Destroy()
    {
        Destroy(gameObject, 0.3f);
    }

}
