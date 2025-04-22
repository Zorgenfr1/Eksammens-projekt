using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public TextMeshProUGUI sensitivityText;
    public TextMeshProUGUI volumeText;

    public static float volume = 1f;
    public static float sensitivity = 1f;

    private void Start()
    {
        sensitivitySlider.value = sensitivity;
        volumeSlider.value = volume;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    private void Update()
    {
        sensitivityText.text = sensitivity.ToString("0.00");
        volumeText.text = volume.ToString("0.00");

        sensitivity = sensitivitySlider.value;
        volume = volumeSlider.value;
    }
}
