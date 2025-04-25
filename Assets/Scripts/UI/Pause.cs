using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject UI;
    public bool isPaused = false;
    public bool isOptions = false;
    public bool isDead = false;

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        UI.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
        UI.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && isPaused == false && isDead == false)
        {
            PauseGame();
        }
        else if(Input.GetKeyUp(KeyCode.Escape) && isOptions == true)
        {
            Resume();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && isOptions == false && isPaused == true && isDead == false)
        {
            ResumeGame();
        }
    }
    public void Options()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        isOptions = true;
    }

    public void Resume()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        isOptions=false;
    }

    public void PlayerDeath()
    {
        isPaused = true;
        isDead = true;
    }
}
