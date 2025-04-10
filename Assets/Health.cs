using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    public delegate void HealthChangedHandler(object source, float oldHealth, float newHealth);
    public event HealthChangedHandler OnHealthChanged;

    public delegate void DeathHandler(object source);
    public event DeathHandler OnDeath;

    public float maxHealth = 100f;
    public bool takingD = false;
    public Canvas healthBar;
    [SerializeField] float currentHealth;

    [SerializeField] float testHealAmount = 10f;
    [SerializeField] float testDamageAmount = -10f;

    public float CurrentHealth => currentHealth;


    public void ChangeHealth(float amount)
    {
        float oldHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    }

    public void Die()
    {

    }
    private void Start()
    {
        healthBar = GetComponent<Canvas>();
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

        if (currentHealth == 0)
        {
            Died?.Invoke();
        }
    }

    public UnityEvent Died;

}
