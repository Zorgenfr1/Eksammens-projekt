using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Deathscreen;
    [SerializeField] private TextMeshProUGUI winTitle;
    [SerializeField] private Escape escape;

    public void PlayerDeath()
    {
        Deathscreen.SetActive(true);
        UI.SetActive(false);
    }

    private void Update()
    {
        if (escape.hasWon == true)
        {
            Deathscreen.SetActive(true);
            UI.SetActive(false);
            winTitle.text = "You've Won";
        }
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
