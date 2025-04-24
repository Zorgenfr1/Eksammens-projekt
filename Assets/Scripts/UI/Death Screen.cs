using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Deathscreen;

    public void PlayerDeath()
    {
        Deathscreen.SetActive(true);
        UI.SetActive(false);
    }

    public void Respawn()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
