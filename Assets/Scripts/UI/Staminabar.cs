using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    [SerializeField] private PlayerController _stamina;
    [SerializeField] private Slider staminaBar;
    private float maxStamina = 50;

    private void Update()
    {
        staminaBar.value = _stamina.stamina / maxStamina;
    }
}
