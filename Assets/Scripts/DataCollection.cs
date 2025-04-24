using UnityEngine;

public class DataCollection : MonoBehaviour
{
    [SerializeField] public static int deaths = 0;
    [SerializeField] public static float timeBeforePlay = 0;
    private bool playing = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            Restart();
        }

        if(playing == false)
        {
            timeBeforePlay += Time.deltaTime;
        }
    }

    public void Died()
    {
        deaths++;
    }

    public void StartedPlay()
    {
        playing = true;
    }

    public void Restart()
    {
        deaths = 0;
        timeBeforePlay = 0;
    }
}
