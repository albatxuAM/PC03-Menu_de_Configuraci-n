using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPausa;
    private bool juegoPausado = false;

    private void Awake()
    {
       
        Reanudar();
    }

    private void OnEnable()
    {
        // Activa el Action Map "PauseInput"
        InputManager.inputControls.PauseInput.Enable();

        // Suscribirse a la acción de Pausa
        InputManager.inputControls.PauseInput.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        // Desactiva el Action Map "PauseInput"
        InputManager.inputControls.PauseInput.Disable();

        // Suscribirse a la acción de Pausa
        InputManager.inputControls.PauseInput.Pause.performed -= OnPause;
    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;  // Reanuda el tiempo
        juegoPausado = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;  // Pausa el tiempo
        juegoPausado = true;
    }

    public void SalirDelJuego()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Función llamada cuando se presiona la tecla de pausa
    private void OnPause(InputAction.CallbackContext context)
    {
        if (juegoPausado)
        {
            Reanudar();
        }
        else
        {
            Pausar();
        }
    }
}
