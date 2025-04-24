using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Deathscreen;
    private bool shouldPause = false;

    public void PlayerDeath()
    {
        Deathscreen.SetActive(true);
        UI.SetActive(false);
        shouldPause = true;
    }

    public void Respawn()
    {
        Deathscreen.SetActive(false);
        UI.SetActive(true);
        shouldPause = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
