using TMPro;
using UnityEngine;

public class DataCollection : MonoBehaviour
{
    public static int deaths = 0;
    private static float timeBeforePlay = 0;
    private float timeToInsideOuterWalls = 0;
    private float timeToCastle = 0;
    private float timeOutOfCastle = 0;
    private float timeToEscape = 0;
    [SerializeField] private OuterWall outerWall;
    [SerializeField] private Castle innerWall;
    [SerializeField] private Escape escape;
    public TextMeshProUGUI deathsText;
    public TextMeshProUGUI timeToWallsText;
    public TextMeshProUGUI timeToCastleText;
    public TextMeshProUGUI timeOutOfCastleText;
    public TextMeshProUGUI timeToEscapeText;

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

        if(innerWall.escapedCastle == false)
        {
            timeOutOfCastle += Time.deltaTime;
        }

        if(escape.escaped == false)
        {
            timeToEscape += Time.deltaTime;
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

        if (innerWall.escapedCastle == true)
        {
            timeOutOfCastleText.text = "Time out of castle " + timeOutOfCastle.ToString("0,0");
        }
        else
        {
            timeOutOfCastleText.text = "N/A";
        }

        if(escape.escaped == true)
        {
            timeToEscapeText.text = "Time to escape " + timeToEscape.ToString("0,0");
        }
        else
        {
            timeToEscapeText.text = "N/A";
        }
    }

    public void Restart()
    {
        deaths = 0;
        timeBeforePlay = 0;
    }
}
