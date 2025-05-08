using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    public int currentScene;

    public void Play()
    {
        currentScene = 0;
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayA()
    {
        currentScene = 1;
        SceneManager.LoadSceneAsync(2);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Resume()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
