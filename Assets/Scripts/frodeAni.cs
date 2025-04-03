using UnityEngine;

public class AimAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isAiming = false;

    void Start()
    {
       
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       
        if (Input.GetMouseButton(1)) 
        {
            if (!isAiming)
            {
                animator.SetTrigger("Aim");
                isAiming = true;
            }
        }
        else
        {
            if (isAiming)
            {
                animator.SetTrigger("Idle");
                isAiming = false;
            }
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
}
