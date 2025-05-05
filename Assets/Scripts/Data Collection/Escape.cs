using UnityEngine;

public class Escape : MonoBehaviour
{
    [SerializeField] private Tresure player;
    [SerializeField] private Pause pause;
    [SerializeField] private DataCollection graphics;
    public bool escaped;
    public bool hasWon;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && player.hasTresure == true)
        {
            escaped = true;
            hasWon = true;

            pause.PlayerDeath();
            DataCollection.deaths--;
            graphics.Died();
        }
    }
}
