using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float potenciaSalto = 5f;

    private Rigidbody rb;
    private Vector2 movimientoInput;
    private bool saltoInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Almacena el valor del movimiento cuando se recibe el input
        movimientoInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Detecta si el botón de salto ha sido presionado
        if (context.performed && rb.velocity.y == 0)  // Solo si está en el suelo
        {
            saltoInput = true;
        }
    }

    private void FixedUpdate()
    {
        // Mover el jugador en el eje X y Z (izquierda/derecha/adelante/atrás)
        Vector3 mover = new Vector3(movimientoInput.x, 0, movimientoInput.y) * velocidadMovimiento * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + mover);

        // Saltar si el botón de salto fue presionado
        if (saltoInput)
        {
            rb.AddForce(Vector3.up * potenciaSalto, ForceMode.Impulse);
            saltoInput = false; // Reinicia el valor para evitar saltos continuos
        }
    }

    public void CambiarVelocidad(float nuevaVelocidad)
    {
        velocidadMovimiento = nuevaVelocidad;
    }

}
