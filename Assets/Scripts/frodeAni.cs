using UnityEngine;
using System.Collections;

public class AimAnimation : MonoBehaviour
{
    private Animator animator;

    [Header("Bow Settings")]
    public int arrows = 2;
    public int arrowCapacity = 30;
    public float fireRate = 1f;

    // Variabler der ændres
    bool _canShoot;
    int _arrowsBack;

    [Header("Weapon Switching")]
    private int currentWeapon = 1;
    private int weaponCount = 3;

    bool _canAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        _arrowsBack = arrowCapacity;
        _canShoot = true;
    }

    void Update()
    {
        // Aiming
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("IsAiming", true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IsAiming", false);
        }

        // Shooting
        if (Input.GetMouseButtonDown(0) && _canShoot && _arrowsBack > 0)
        {
            animator.SetTrigger("Attack");
            _canShoot = false;
            _arrowsBack--;
            StartCoroutine(Shoot());
        }

        //Sword & Knife attack
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            animator.SetTrigger("Attack");
        }

        // Scroll Weapon Switching
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if (scroll > 0)
                currentWeapon = (currentWeapon % weaponCount) + 1;
            else
                currentWeapon = (currentWeapon - 2 + weaponCount) % weaponCount + 1;


            PlayWeaponScrollAnimation(currentWeapon);
        }

        // Quick key selection (optional)
        if (Input.GetKeyDown(KeyCode.Alpha1)) { currentWeapon = 1; PlayWeaponScrollAnimation(1); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { currentWeapon = 2; PlayWeaponScrollAnimation(2); _canAttack = true; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { currentWeapon = 3; PlayWeaponScrollAnimation(3); _canAttack = true; }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(fireRate);
        animator.SetTrigger("Idle");
        _canShoot = true;
    }

    void PlayWeaponScrollAnimation(int weaponIndex)
    {
        switch (weaponIndex)
        {
            case 1:
                animator.SetTrigger("Scroll 1");
                break;
            case 2:
                animator.SetTrigger("Scroll 2");
                break;
            case 3:
                animator.SetTrigger("Scroll 3");
                break;
            default:
                animator.SetTrigger("Scroll 1"); 
                break;
        }
    }
}
