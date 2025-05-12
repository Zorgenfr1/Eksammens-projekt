using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordHitPlayer : MonoBehaviour
{
    public Attacks lightAttack;
    public GameObject mainCamera;
    private Vector3 spawnPosition;
    private AudioSource swordAudio;
    public bool shakeStart = false;
    public float shakeDuration = 0.2f;
    public AnimationCurve curve;

    private void Start()
    {
        swordAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 3f))
            {
                spawnPosition = hit.point;
                spawnPosition.y += -0.2f;
            }

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.ChangeHealth(-lightAttack.damage);
            swordAudio.PlayOneShot(lightAttack.hitSound);
            Instantiate(lightAttack.bloodEffect, spawnPosition, Quaternion.identity, other.transform);
            //shakeStart = true;
        }
    }

    /*private void Update()
    {
        if (shakeStart)
        {
            shakeStart = false;
            StartCoroutine(Shaking());
        }
        
    }

    IEnumerator Shaking()
    {
        Vector3 startShakePosition = mainCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float shakeStrength = curve.Evaluate(elapsedTime / shakeDuration);
            mainCamera.transform.position = startShakePosition + Random.insideUnitSphere * shakeStrength;
            yield return null;
        }
        mainCamera.transform.position = startShakePosition;
    } */

}
    