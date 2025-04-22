using UnityEngine;
using System.Collections;

public class AimAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isAiming = false;

    [Header("Bow Settings")]
    public int arrows = 2;
    public int arrowCapacity = 30;
    public float fireRate = 1f;

    //Variabler der ændres
    bool _canShoot;
    int _arrowsBack;

    void Start()
    {
       
        animator = GetComponent<Animator>();
        _arrowsBack = arrowCapacity;
        _canShoot = true;
    }

    void Update()
    {
       
        if (Input.GetMouseButton(1)) 
        {
           animator.SetBool("IsAiming", true);
           isAiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IsAiming", false);
            isAiming = false;
        }
        

        if (Input.GetMouseButtonDown(0) && _canShoot && _arrowsBack > 0)
        {
            animator.SetTrigger("Shoot");
            _canShoot = false;
            _arrowsBack--;
            StartCoroutine(Shoot());
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0)
        {
            animator.SetTrigger("ScrollUp");
        }
        else if (scroll < 0)
        {
            animator.SetTrigger("ScrollDown");
        }


    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Idle");
        _canShoot = true;
    }
}
