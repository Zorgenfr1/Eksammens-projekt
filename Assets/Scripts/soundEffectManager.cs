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
        Debug.Log("Equipping Sword");
    }

    void UnEquipSword()
    {
        equipSword.SetActive(false);
        unEquipSword.SetActive(true);
        Debug.Log("Unequippng Sword");
    }
}

