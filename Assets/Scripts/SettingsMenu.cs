using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider velocidadSlider;
    public PlayerController jugador;

    void Start()
    {
        velocidadSlider.onValueChanged.AddListener(CambiarVelocidadJugador);
    }

    public void CambiarVelocidadJugador(float valor)
    {
        jugador.CambiarVelocidad(valor);
    }
}
