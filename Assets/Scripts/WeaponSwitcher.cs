using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject bow;   // Buen sidder i venstre hånd
    public GameObject sword; // Sværdet sidder i højre hånd
    public Animator animator;

    private int currentWeapon = 0; // 0 = Bue (venstre hånd), 1 = Sværd (højre hånd)


    private void Start()
    {
        bow.SetActive(true);
        sword.SetActive(false);
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0 || scroll < 0) // Skifter våben ved scroll
        {
            currentWeapon = (currentWeapon + 1) % 2; // Skifter mellem 0 og 1
        }
    }

    public void ShowBow()
    {
        bow.SetActive(true);  // Gør buen synlig
    }

    public void HideBow()
    {
        bow.SetActive(false); // Skjuler buen
    }

    public void ShowSword()
    {
        sword.SetActive(true); // Gør sværdet synligt
    }

    public void HideSword()
    {
        sword.SetActive(false); // Skjuler sværdet
    }
}
