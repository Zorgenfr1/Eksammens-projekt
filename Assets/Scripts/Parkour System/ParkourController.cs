using System.Collections;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    bool inAction;
    
    EnviromentScanner enviromentScanner;
    public Animator animator;


    private void Awake()
    {
        enviromentScanner = GetComponent<EnviromentScanner>();
    }

    private void Update()
    {
        var hitData = enviromentScanner.ObstacleCheck();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hitData.forwardHitFound && !inAction)
            {
                StartCoroutine(DoParkourAction());
            }
        }

        IEnumerator DoParkourAction()
        {
            inAction = true;
            animator.CrossFade("Stepup", 0.2f);
            yield return null;

            var animState = animator.GetNextAnimatorStateInfo(0);

            yield return new WaitForSeconds(animState.length);

            inAction = false;
        }
    }
}
    