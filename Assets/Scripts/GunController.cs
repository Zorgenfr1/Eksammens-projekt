using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    [Header("Bow Settings")]
    public int arrows = 2;
    public int arrowCapacity = 30;
    public float fireRate = 1f;

    //Variabler der ændres
    bool _canShoot;
    int _arrowsBack;

    [Header("Bullet Settings")]
    public GameObject arrowPrefab;
    public Transform shootingPoint;
    

    [Header("Aim Settings")]
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10f;
    public float requiredAimTime = 2f;
    public Transform cameraPivot;

    //Variabler der ændres
    float _aimTime = 0f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1f;
    Vector2 _currentRotation;
    public float weaponSwayAmount = 10f;
    //public Animator animator;

    [Header("Recoil Settings")]
    //våben recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    //kun hvis ramdon recoil er slået fra, idk kommer fra videoen
    public Vector2 recoilPattern;


    private void Start()
    {
        _arrowsBack = arrowCapacity;
        _canShoot = true;
        Debug.Log("Started Bow");

    }

    private void Update()
    {
        DetermineAim();

        if (Input.GetMouseButton(0) && _canShoot && _aimTime >= requiredAimTime)
        {
            Debug.Log("kumulala Sawesta");
            _canShoot =false;
            _arrowsBack--;
            StartCoroutine(Shoot());
            Debug.Log("Tung Tung Tung");
        }
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;

        if (Input.GetMouseButton(1))
        {
            target = aimingLocalPosition;
            _aimTime += Time.deltaTime;
        }
        else
        {
            _aimTime = 0f;
        }

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
    }

    IEnumerator Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootingPoint.position, shootingPoint.rotation * Quaternion.Euler(0, 90, 0));
        arrow.GetComponent<Rigidbody>().AddForce(shootingPoint.forward * 30f, ForceMode.Impulse);
        Debug.DrawRay(shootingPoint.position, shootingPoint.forward * 5, Color.red, 7f);
        Debug.Log("Shoot fired");
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
}
