using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{


    public delegate void DeathHandler(object source);
    public event DeathHandler OnDeath;

    public float maxHealth = 100f;
    public bool takingD = false;
    public Slider healthBar;
    [SerializeField] float currentHealth;

    [SerializeField] float testHealAmount = 10f;
    [SerializeField] float testDamageAmount = -10f;
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
        Debug.Log("Changed health by" + amount);
        StartCoroutine(Invincibility());


    }

    IEnumerator Invincibility()
    {
        Debug.Log("invincible");
        isInvincible = true;
        yield return new WaitForSeconds(iFrameTime);
        Debug.Log("no longer invincible");
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
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeHealth(testHealAmount);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeHealth(testDamageAmount);
        }

        if (currentHealth == 0 && isLiving)
        {
            Died?.Invoke();
            isLiving = false;
        }
    }

    public UnityEvent Died;

}