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

    private void OnEnable()
    {
        // Activa el Action Map "Player"
        InputManager.inputControls.PlayerInput.Enable();

        // Suscribirse a los eventos de input
        InputManager.inputControls.PlayerInput.Move.performed += OnMove;
        InputManager.inputControls.PlayerInput.Move.canceled += OnMove;
        InputManager.inputControls.PlayerInput.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        // Desactiva el Action Map "Player"
        InputManager.inputControls.PlayerInput.Disable();

        // Desuscribirse de los eventos de input
        InputManager.inputControls.PlayerInput.Move.performed -= OnMove;
        InputManager.inputControls.PlayerInput.Move.canceled -= OnMove;
        InputManager.inputControls.PlayerInput.Jump.performed -= OnJump;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movimientoInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && rb.velocity.normalized.y == 0)
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
