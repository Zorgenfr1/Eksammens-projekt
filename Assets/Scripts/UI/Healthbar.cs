using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider healthBar;

    private void Update()
    {
        healthBar.value = _health.CurrentHealth / _health.maxHealth;
    }
}
