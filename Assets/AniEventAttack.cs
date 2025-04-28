using UnityEngine;

public class AniEventAttack : MonoBehaviour
{
    public GameObject swordHitCollider;
    public GameObject DaggerHitCollider;

    void ActivateSwordCollider()
    {
        swordHitCollider.SetActive(true);
    }

    void DeActivateSwordCollider()
    {
        swordHitCollider.SetActive(false);
    }
    void ActivateDaggerCollider()
    {
        DaggerHitCollider.SetActive(true);
    }

    void DeActivateDaggerCollider()
    {
        DaggerHitCollider.SetActive(false);
    }
}
