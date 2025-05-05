using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject bow;
    public GameObject sword;
    public GameObject knife;
    public Animator animator;
    public GameObject crosshair;

    private void Start()
    {
        bow.SetActive(true);
        crosshair.SetActive(true);
        sword.SetActive(false);
        knife.SetActive(false);
    }

    public void ShowBow()
    {
        bow.SetActive(true);
        crosshair.SetActive(true);
    }

    public void HideBow()
    {
        bow.SetActive(false);
        crosshair.SetActive(false);
    }

    public void ShowSword()
    {
        sword.SetActive(true);
    }

    public void HideSword()
    {
        sword.SetActive(false);
    }

    public void ShowKnife()
    {
        knife.SetActive(true);
    }

    public void HideKnife()
    {
        knife.SetActive(false);
    }
}