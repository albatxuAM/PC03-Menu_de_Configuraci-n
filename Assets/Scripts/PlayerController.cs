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

    private CustomControls inputControls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputControls = new CustomControls();
    }

    private void OnEnable()
    {
        // Activa el Action Map "Player"
        inputControls.Player.Enable(); 

        // Suscribirse a los eventos de input
        inputControls.Player.Move.performed += OnMove;
        inputControls.Player.Move.canceled += OnMove;
        inputControls.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        // Desactiva el Action Map "Player"
        inputControls.Player.Disable();  

        // Desuscribirse de los eventos de input
        inputControls.Player.Move.performed -= OnMove;
        inputControls.Player.Move.canceled -= OnMove;
        inputControls.Player.Jump.performed -= OnJump;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movimientoInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && rb.velocity.y == 0)
        {
            saltoInput = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 mover = new Vector3(movimientoInput.x, 0, movimientoInput.y) * velocidadMovimiento * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + mover);

        if (saltoInput)
        {
            rb.AddForce(Vector3.up * potenciaSalto, ForceMode.Impulse);
            saltoInput = false;
        }
    }

    public void CambiarVelocidad(float nuevaVelocidad)
    {
        velocidadMovimiento = nuevaVelocidad;
    }
}
