using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject sideHUD;
    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        sideHUD.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        sideHUD.SetActive(true);
        Time.timeScale = 1f;
    }
}