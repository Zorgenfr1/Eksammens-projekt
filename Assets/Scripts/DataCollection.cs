using UnityEngine;

public class DataCollection : MonoBehaviour
{
    [SerializeField] public static int deaths = 0;
    [SerializeField] public static float time = 0;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            Restart();
        }

        time += Time.deltaTime;
    }

    public void Died()
    {
        deaths++;
    }

    public void Restart()
    {
        deaths = 0;
        time = 0;
    }
}
