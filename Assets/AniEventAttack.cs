using UnityEngine;

public class AniEventAttack : MonoBehaviour
{
    public GameObject swordHitCollider;

    void ActivateSwordCollider()
    {
        swordHitCollider.SetActive(true);
    }

    void DeActivateSwordCollider()
    {
        swordHitCollider.SetActive(false);
    }
}
