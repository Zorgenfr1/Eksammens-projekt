using UnityEngine;
using UnityEngine.UIElements;

public class SwordHitPlayer : MonoBehaviour
{
    public Attacks lightAttack;
    public GameObject mainCamera;
    private Vector3 spawnPosition;
    private AudioSource swordAudio;

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
                Debug.Log("Raycast hit");
                spawnPosition = hit.point;
                spawnPosition.y += -0.2f;
            }
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.ChangeHealth(-lightAttack.damage);
            swordAudio.PlayOneShot(lightAttack.hitSound);
            Instantiate(lightAttack.bloodEffect, spawnPosition, Quaternion.identity, other.transform);
            Debug.Log(spawnPosition);

        }
    }

}
    