using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject bow;   // Buen sidder i venstre h�nd
    public GameObject sword; // Sv�rdet sidder i h�jre h�nd
    public Animator animator;

    private int currentWeapon = 0; // 0 = Bue (venstre h�nd), 1 = Sv�rd (h�jre h�nd)


    private void Start()
    {
        bow.SetActive(true);
        sword.SetActive(false);
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0 || scroll < 0) // Skifter v�ben ved scroll
        {
            currentWeapon = (currentWeapon + 1) % 2; // Skifter mellem 0 og 1
        }
    }

    public void ShowBow()
    {
        bow.SetActive(true);  // G�r buen synlig
    }

    public void HideBow()
    {
        bow.SetActive(false); // Skjuler buen
    }

    public void ShowSword()
    {
        sword.SetActive(true); // G�r sv�rdet synligt
    }

    public void HideSword()
    {
        sword.SetActive(false); // Skjuler sv�rdet
    }
}
