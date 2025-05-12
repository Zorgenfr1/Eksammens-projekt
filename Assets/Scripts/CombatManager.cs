using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Attacks lightAttackData;
    public Attacks heavyAttackData;
    public Attacks currentAttackData;

    [Header("Attack Buttons")]
    public KeyCode lightAttackButton;
    public KeyCode heavyAttackButton;

    public AudioSource audio;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(lightAttackButton))
        {
            currentAttackData = lightAttackData;
            Attack();
        }
        
        if (Input.GetKeyDown(heavyAttackButton))
        {
            currentAttackData = heavyAttackData;
            Attack();
        }
    }

    public void Attack()
    {
        audio.PlayOneShot(currentAttackData.hitSound);


    }
}
