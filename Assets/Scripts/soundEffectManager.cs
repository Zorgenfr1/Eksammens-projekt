using UnityEngine;

public class soundEffectManager : MonoBehaviour
{
    public AudioSource audio;
    public Attacks lightAttack;

    public GameObject equipSword;
    public GameObject unEquipSword;

    public GameObject equipShield;
    public GameObject unEquipShield;
    void PlaySoundEffect()
    {
        audio.PlayOneShot(lightAttack.hitSound);
    }

    void EquipSword()
    {
        equipSword.SetActive(true);
        unEquipSword.SetActive(false);
    }

    void UnEquipSword()
    {
        equipSword.SetActive(false);
        unEquipSword.SetActive(true);
    }
}

