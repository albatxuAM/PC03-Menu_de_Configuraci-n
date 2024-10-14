using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    private void Awake()
    {
        // Desactivar el menú de pausa al inicio
        pauseMenu.SetActive(false);
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        // Pausa el tiempo
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        // Reanuda el tiempo
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        // Asegúrate de reanudar el tiempo
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu_Level");
    }
}
