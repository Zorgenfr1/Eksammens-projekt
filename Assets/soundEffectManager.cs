using UnityEngine;

public class soundEffectManager : MonoBehaviour
{
    public AudioSource audio;
    public Attacks lightAttack;
    void PlaySoundEffect()
    {
        audio.PlayOneShot(lightAttack.hitSound);
    }
}
