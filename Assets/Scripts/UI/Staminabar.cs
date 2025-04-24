using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    [SerializeField] private PlayerController _stamina;
    [SerializeField] private Slider staminaBar;
    private float maxStamina;

    private void Start()
    {
        maxStamina = _stamina.stamina;
    }

    private void Update()
    {
        staminaBar.value = _stamina.stamina / maxStamina;
    }
}
