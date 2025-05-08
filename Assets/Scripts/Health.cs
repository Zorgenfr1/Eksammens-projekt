using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Health : MonoBehaviour
{


    public delegate void DeathHandler(object source);
    public event DeathHandler OnDeath;

    public float maxHealth = 100f;
    public bool takingD = false;
    public Slider healthBar;
    [SerializeField] float currentHealth;
    [SerializeField] private GameObject damageIndication;

    [SerializeField] float testHealAmount = 10f;
    [SerializeField] float testDamageAmount = -10f;
    public bool isInvincible = false;
    public float iFrameTime;
    public bool isLiving = true;
    private float timeSinceDamage = 1;

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

        timeSinceDamage = 0.20f;
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
        damageIndication.SetActive(false);
    }

    private void Update()
    {

        if (currentHealth == 0 && isLiving)
        {
            Died?.Invoke();
            isLiving = false;
        }

        timeSinceDamage += Time.deltaTime;

        if(timeSinceDamage <= 0.15f)
        {
            damageIndication.SetActive(true);
        }
        else
        {
            damageIndication.SetActive(false);
        }
    }

    public UnityEvent Died;

}
