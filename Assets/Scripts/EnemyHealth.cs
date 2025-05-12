using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{


    public delegate void DeathHandler(object source);

    public float maxHealth = 100f;
    public bool takingD = false;
    public Slider healthBar;
    [SerializeField] float currentHealth;

    public Animator guardAni;

    public bool isInvincible = false;
    public float iFrameTime = 1f;
    public bool isLiving = true;

    public float CurrentHealth => currentHealth;


    public void ChangeHealth(float amount)
    {
        if (isInvincible)
        {
            return;
        }

        float oldHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        guardAni.SetTrigger("Gooner");
        StartCoroutine(Invincibility());


    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(iFrameTime);
        isInvincible = false;

    }

    public void Die()
    {

    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {

        if (currentHealth == 0 && isLiving)
        {
            Died?.Invoke();
            isLiving = false;
        }
    }

    public UnityEvent Died;

}