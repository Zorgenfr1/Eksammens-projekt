using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Attacks", menuName = "MeleeAttacks")]
public class Attacks : ScriptableObject
{
    public float damage;
    //public VisualEffect effect;
    public AnimatorOverrideController AOC;
    public AudioClip hitSound;
    public GameObject bloodEffect;
}

