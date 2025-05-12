using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    [Header("Bow Settings")]
    public int arrows = 2;
    public int arrowCapacity = 30;
    public float fireRate = 1f;
    bool _canShoot;
    int _arrowsBack;

    [Header("Bullet Settings")]
    public GameObject arrowPrefab;
    public Transform shootingPoint;
    

    [Header("Aim Settings")]
    /*public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10f;*/
    public float requiredAimTime = 2f;
    public Transform cameraPivot;
    float _aimTime = 0f;

    /*[Header("Mouse Settings")]
    public float mouseSensitivity = 1f;
    Vector2 _currentRotation;
    public float weaponSwayAmount = 10f;
    //public Animator animator;

    [Header("Recoil Settings")]
    //våben recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    //kun hvis ramdon recoil er slået fra, idk kommer fra videoen
    public Vector2 recoilPattern;*/


    private void Start()
    {
        _arrowsBack = arrowCapacity;
        _canShoot = true;

    }

    private void Update()
    {
        DetermineAim();

        //DetermineRotation();

        if (Input.GetMouseButton(0) && _canShoot && _aimTime >= requiredAimTime)
        {
            //animator.SetTrigger("Shoot");
            _canShoot =false;
            _arrowsBack--;
            //animator.SetTrigger("Idle");
            StartCoroutine(Shoot());
        }
    }

    void DetermineAim()
    {
        /*Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;*/

        if (Input.GetMouseButton(1))
        {
            //target = aimingLocalPosition;
            _aimTime += Time.deltaTime; 
        }
        else
        {
            _aimTime = 0f;
        }

        //Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        //transform.localPosition = desiredPosition;
    }

    /*void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;

        if (randomizeRecoil)
        {
            float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
            float yRecoil = Random.Range(-randomRecoilConstraints.y, randomRecoilConstraints.y);

            Vector2 recoil = new Vector2(xRecoil, yRecoil);

            _currentRotation += recoil;
        }
    }*/

    IEnumerator Shoot()
    {
        //DetermineRecoil();
        GameObject arrow = Instantiate(arrowPrefab, shootingPoint.position, shootingPoint.rotation * Quaternion.Euler(0, -90, 0));
        arrow.GetComponent<Rigidbody>().AddForce(shootingPoint.forward * 30f, ForceMode.Impulse);
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
}
