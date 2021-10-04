using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{

    public GameObject ui;
    public Text menuButtonText;
    public bool isMenuOpen = false;

    public void Open(string buttonName)
    {
        menuButtonText.text = buttonName;
        ui.SetActive(true);
        isMenuOpen = true;
        Time.timeScale = 0f;
    }

    public void Close()
    {
        ui.SetActive(false);
        isMenuOpen = false;
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
