using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    public bool paused = false;
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        paused = false;
        pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            PauseGame();
        }
    }
    public void Options()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Resume()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
