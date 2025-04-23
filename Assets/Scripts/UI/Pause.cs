using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    public bool isPaused = false;
    public bool isOptions = false;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && isPaused == false)
        {
            PauseGame();
        }
        else if(Input.GetKeyUp(KeyCode.Escape) && isOptions == true)
        {
            Resume();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && isOptions == false && isPaused == true)
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
}
