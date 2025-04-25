using TMPro;
using UnityEngine;

public class DataCollection : MonoBehaviour
{
    public static int deaths = 0;
    public static float timeBeforePlay = 0;
    public float timeToInsideOuterWalls = 0;
    public float timeToCastle = 0;
    [SerializeField] private OuterWall outerWall;
    [SerializeField] private Castle innerWall;
    public TextMeshProUGUI deathsText;
    public TextMeshProUGUI timeToWallsText;
    public TextMeshProUGUI timeToCastleText;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            Restart();
        }

        if(outerWall.insideTheOuterWall == false)
        {
            timeToInsideOuterWalls += Time.deltaTime;
        }

        if(innerWall.insideCastle == false)
        {
            timeToCastle += Time.deltaTime;
        }
    }

    public void Died()
    {
        deaths++;
        deathsText.text = deaths.ToString();
    }

    public void Restart()
    {
        deaths = 0;
        timeBeforePlay = 0;
    }
}
