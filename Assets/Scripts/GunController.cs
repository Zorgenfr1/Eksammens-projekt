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
    

    [Header("Aim Settings")]
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10f;
    public float requiredAimTime = 2f;

    //Variabler der ændres
    float _aimTime = 0f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1f;
    Vector2 _currentRotation;
    public float weaponSwayAmount = 10f;

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

    }

    private void Update()
    {
        DetermineAim();

        DetermineRotation();

        if (Input.GetMouseButton(0) && _canShoot && _aimTime >= requiredAimTime)
        {
            _canShoot=false;
            _arrowsBack--;
            StartCoroutine(Shoot());
        }
    }

    void DetermineRotation()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        _currentRotation += mouseAxis;

        _currentRotation.y = Mathf.Clamp(_currentRotation.y, -90, 90);

        transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;

        transform.root.localRotation = Quaternion.AngleAxis(_currentRotation.x, Vector3.up);
        transform.parent.localRotation = Quaternion.AngleAxis(- _currentRotation.y, Vector3.right);
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;

        if (Input.GetMouseButton(1))
        {
            target = aimingLocalPosition;
            _aimTime += Time.deltaTime; // Øg tid mens sigtes
        }
        else
        {
            _aimTime = 0f; // Nulstil hvis ikke sigter
        }

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
    }

    void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;

        if (randomizeRecoil)
        {
            float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
            float yRecoil = Random.Range(-randomRecoilConstraints.y, randomRecoilConstraints.y);

            Vector2 recoil = new Vector2(xRecoil, yRecoil);

            _currentRotation += recoil;
        }
    }

    IEnumerator Shoot()
    {
        DetermineRecoil();
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
}
