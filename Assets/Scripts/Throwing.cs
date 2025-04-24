using UnityEngine;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public PlayerController playerController;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public KeyCode AimKey = KeyCode.Mouse1;
    public float throwForceMin;
    public float throwForceMax;
    public float trueThrowForce;
    public float chargeTime;
    public float lerpSpeed;
    public float throwUpwardForce;

    bool readyToThrow;

    void Start()
    {
        readyToThrow = true;
    }

    void Update()
    {
        ThrowForceCalculation();
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void ThrowForceCalculation()
    {
        if (Input.GetKey(AimKey))
        {
            trueThrowForce = Mathf.Lerp(throwForceMin, throwForceMax, chargeTime);
            chargeTime += Time.deltaTime * lerpSpeed;
            Debug.Log("Charging");
        }
        else
        {
            trueThrowForce = throwForceMin;
            chargeTime = 0;
        }
    }
    
    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        //Vector3 inheritedVelocity = playerController.GetCurrentFlatVelocity();

        //Vector3 forceToAdd = cam.transform.forward * trueThrowForce + transform.up * throwUpwardForce + inheritedVelocity;

        //projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
