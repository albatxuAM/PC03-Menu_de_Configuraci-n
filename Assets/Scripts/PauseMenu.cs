using TMPro; 
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;                // Menú de pausa
    public TextMeshProUGUI velocidadText;       // Texto para mostrar la velocidad del jugador
    public Slider velocidadSlider;              // Slider para ajustar la velocidad del jugador
    public PlayerController jugador;            // Referencia al controlador del jugador
    public TMP_InputField saltoInputField;      // Input field para controlar el salto

    private bool isPaused = false;

    private void Awake()
    {
        // Desactivar el menú de pausa al inicio
        pauseMenu.SetActive(false);

        // Asegurarse de que el valor inicial del slider coincida con la velocidad del jugador
        velocidadSlider.value = jugador.GetVelocidad();
        velocidadSlider.onValueChanged.AddListener(CambiarVelocidadJugador);

        // Actualizar el input field del salto con el valor actual del jugador
        saltoInputField.text = jugador.GetSalto().ToString("F1");
        saltoInputField.onEndEdit.AddListener(CambiarSaltoJugador); // Ejecutar cuando se termine de editar el valor
    }


    // Callback del sistema de input para pausar/reanudar el juego
    public void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    // Pausar el juego
    public void PauseGame()
    {
        isPaused = true;
        // Pausa el tiempo
        Time.timeScale = 0f;
        // Actualizar el valor del slider al abrir el menú de pausa
        velocidadSlider.value = jugador.GetVelocidad();
        // Actualizar el texto de velocidad
        ActualizarVelocidadTexto(jugador.GetVelocidad());

        saltoInputField.text = jugador.GetSalto().ToString("F1");

        pauseMenu.SetActive(true);
    }

    // Reanudar el juego
    public void ResumeGame()
    {
        isPaused = false;
        // Reanuda el tiempo
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    // Cambiar la velocidad del jugador cuando se ajusta el slider
    public void CambiarVelocidadJugador(float valor)
    {
        jugador.CambiarVelocidad(valor);
        // Actualizar el texto con el nuevo valor de velocidad
        ActualizarVelocidadTexto(valor);
    }

    // Cambiar el salto del jugador cuando se modifica el input field
    public void CambiarSaltoJugador(string valorInput)
    {
        if (float.TryParse(valorInput, out float valorSalto))
        {
            jugador.CambiarSalto(valorSalto);
        }
    }

    // Actualizar el texto de velocidad en el menú
    private void ActualizarVelocidadTexto(float valor)
    {
        velocidadText.text = "Velocidad: " + valor.ToString("F1");
    }

    public void SalirDelJuego()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
