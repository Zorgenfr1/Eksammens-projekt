using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Options()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
