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
        deathsText.text = "Deaths "+deaths.ToString();
        if(outerWall.insideTheOuterWall == true)
        {
            timeToWallsText.text = "Time to wall " + timeToInsideOuterWalls.ToString("0.0");
        }
        else
        {
            timeToWallsText.text = "N/A";
        }

        if(innerWall.insideCastle == true)
        {
            timeToCastleText.text = "Time to castle " + timeToCastle.ToString("0.0");
        }
        else
        {
            timeToCastleText.text = "N/A";
        }
    }

    public void Restart()
    {
        deaths = 0;
        timeBeforePlay = 0;
    }
}
