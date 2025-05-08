using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject bow;
    public GameObject sword;
    public GameObject knife;
    public Animator animator;
    public GameObject crosshair;
    public AudioClip attack1Sound;
    public AudioClip attack2Sound;
    public AudioClip daggerSound;
    private AudioSource playerAudio;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
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

    public void AttackSound1()
    {
        playerAudio.PlayOneShot(attack1Sound);
    }

    public void AttackSound2()
    {
        playerAudio.PlayOneShot(attack2Sound);
    }

    public void DaggerSound()
    {
        playerAudio.PlayOneShot(daggerSound);
    }
}