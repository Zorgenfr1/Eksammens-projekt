using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    [Header("Bow Settings")]
    public float fireRate = 2f;
    public int arrows = 1;
    public int arrowCapacity = 30;

    //Variabler der ændres
    bool _canShoot;
    int _arrowsBack;

    //Aiming
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1f;
    Vector2 _currentRotation;
    public float weaponSwayAmount = 10f;


    private void Start()
    {
        _arrowsBack = arrowCapacity;
        _canShoot = true;

    }

    private void Update()
    {
        DetermineAim();

        DetermineRotation();

        if (Input.GetMouseButton(0) && _canShoot)
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

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
}
